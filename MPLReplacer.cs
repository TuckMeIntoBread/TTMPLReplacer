using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Ionic.Zip;
using Ionic.Zlib;
using Newtonsoft.Json;
using TTMPLReplacer.Enums;

namespace TTMPLReplacer
{
    public static class MPLReplacer
    {
        public static bool ConvertFile(string filePath)
        {
            try
            {
                Program.Log($"Converting '{Path.GetFileName(filePath)}'!");
                Stopwatch sw = Stopwatch.StartNew();
                using (ZipFile zip = ZipFile.Read(filePath))
                {
                    ZipEntry mpl = zip.Entries.First(x => x.FileName.EndsWith(".mpl"));
                    ModPackJson jsonData;
                    using (StreamReader stream = new(mpl.OpenReader()))
                    {
                        jsonData = JsonConvert.DeserializeObject<ModPackJson>(stream.ReadToEnd()) ?? throw new InvalidOperationException(".mpl returned null json data!");
                    }

                    foreach (ModsJson modsJson in jsonData.AllModsJsons)
                    {
                        ConvertPath(modsJson);
                    }
                    
                    jsonData.RemoveMaterials();
                    
                    zip.RemoveEntry(mpl);
                    zip.AddEntry(mpl.FileName, JsonConvert.SerializeObject(jsonData));
                    zip.UseZip64WhenSaving = Zip64Option.AsNecessary;
                    zip.CompressionLevel = CompressionLevel.None;
                    if (!Directory.Exists(Program.ConvertedFolder)) Directory.CreateDirectory(Program.ConvertedFolder);
                    zip.Save(Path.Combine(Program.ConvertedFolder, Path.GetFileName(filePath)));
                    sw.Stop();
                    Program.Log($"Converted '{Path.GetFileName(filePath)}' in {sw.ElapsedMilliseconds:n0}ms." + Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                Program.Log($"Something went wrong with converting '{Path.GetFileName(filePath)}'!" + Environment.NewLine);
                Program.Log(e.ToString());
                return false;
            }

            return true;
        }

        private static void ConvertPath(ModsJson modsJson)
        {
            if (modsJson is null) throw new ArgumentNullException(nameof(modsJson));
            if (modsJson.FullPath is null) throw new ArgumentNullException(nameof(modsJson.FullPath), $"{modsJson.Name} has no FullPath!");
            if (!TryGetValidPathData(modsJson, out PathData pathData)) return;

            if (ReplaceDictionary.TryGetReplacementFileName(pathData, out string replacementFile))
            {
                string prevPath = modsJson.FullPath;
                switch (Program.ConvertType)
                {
                    case ConvertType.Bibo:
                        modsJson.FullPath = $"chara/bibo/{replacementFile}";
                        break;
                    case ConvertType.Gen3:
                        modsJson.FullPath = $"{pathData.Path}{replacementFile}";
                        break;
                }
                Program.Log($"Converted {modsJson.Name} from '{prevPath}' to '{modsJson.FullPath}'");
                return;
            }
            
            Program.Log($"Did not convert {pathData}.");
        }

        private static bool TryGetValidPathData(ModsJson modsJson, out PathData pathData)
        {
            void LogFailed(string reason)
            {
                Program.Log($"Skipping '{modsJson.Name}' : '{modsJson.FullPath}' : Reason: {reason}");
            }

            pathData = null;
            if (!modsJson.FullPath.StartsWith("chara/human/", StringComparison.OrdinalIgnoreCase))
            {
                LogFailed("Not a chara/human/ path.");
                return false;
            }
            
            if (!modsJson.FullPath.EndsWith(".tex", StringComparison.OrdinalIgnoreCase))
            {
                LogFailed("Not a .tex file.");
                return false;
            }
            
            if (!modsJson.FullPath.Contains("/body/", StringComparison.OrdinalIgnoreCase))
            {
                LogFailed("Not a body path.");
                return false;
            }

            if (!modsJson.FullPath.Contains("/texture/", StringComparison.OrdinalIgnoreCase))
            {
                LogFailed("Not a texture path.");
                return false;
            }

            pathData = new PathData(modsJson);
            if (pathData.IsValid != ValidCheck.Valid)
            {
                LogFailed(pathData.IsValid.ToString());
                return false;
            }

            return true;
        }
    }
}