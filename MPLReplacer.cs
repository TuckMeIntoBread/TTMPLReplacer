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
                    zip.AddEntry("CONVERTED.txt", "Converted using TTMPLReplacer made by Bread and Bizu.");
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

            string prevPath;
            if (ReplacerForm.CorrectMatA && pathData.IsValid == ValidCheck.Gen2Tex)
            {
                prevPath = modsJson.FullPath;
                modsJson.FullPath = $"{pathData.Path}--c{pathData.RaceCode.ConvertCode()}b{pathData.TypeCode.ConvertCode()}_{pathData.TexType}.tex";
                Program.Log($"Converted {modsJson.Name} from '{prevPath}' to '{modsJson.FullPath}'");
                return;
            }

            if (!ReplaceDictionary.TryGetReplacementFileName(pathData, out string replacementFile)) return;

            prevPath = modsJson.FullPath;
            if (pathData.IsBiboConvert)
            {
                modsJson.FullPath = $"chara/bibo/{replacementFile}";
            }
            else if (pathData.IsGen3Convert)
            {
                modsJson.FullPath = $"{pathData.Path}{replacementFile}";
            }
            else
            {
                Program.Log($"Did not convert {pathData}.");
                return;
            }
            Program.Log($"Converted {modsJson.Name} from '{prevPath}' to '{modsJson.FullPath}'");
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
            
            if (!modsJson.FullPath.Contains("/texture/", StringComparison.OrdinalIgnoreCase))
            {
                LogFailed("Not a texture path.");
                return false;
            }
            
            if (!modsJson.FullPath.Contains("/body/", StringComparison.OrdinalIgnoreCase))
            {
                LogFailed("Not a body path.");
                return false;
            }

            pathData = new PathData(modsJson);
            if (ReplacerForm.CorrectMatA && pathData.IsValid == ValidCheck.Gen2Tex)
            {
                return true;
            }
            
            if (pathData.IsValid != ValidCheck.Valid)
            {
                LogFailed(pathData.IsValid.ToString());
                return false;
            }

            return true;
        }

        private static string ConvertCode(this int code)
        {
            return code switch
            {
                > 1000 => code.ToString(),
                > 100 => $"0{code.ToString()}",
                > 10 => $"00{code.ToString()}",
                _ => $"000{code.ToString()}"
            };
        }
    }
}