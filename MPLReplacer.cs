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
                Program.Log($"Converting {Path.GetFileName(filePath)}");
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
                    
                    zip.RemoveEntry(mpl);
                    zip.AddEntry(mpl.FileName, JsonConvert.SerializeObject(jsonData));
                    zip.UseZip64WhenSaving = Zip64Option.AsNecessary;
                    zip.CompressionLevel = CompressionLevel.None;
                    if (!Directory.Exists(Program.ConvertedFolder)) Directory.CreateDirectory(Program.ConvertedFolder);
                    zip.Save(Path.Combine(Program.ConvertedFolder, Path.GetFileName(filePath)));
                    sw.Stop();
                    Program.Log($"Converted {Path.GetFileName(filePath)} in {sw.ElapsedMilliseconds:n0}ms.");
                }
            }
            catch (Exception e)
            {
                Program.Log($"Something went wrong with converting {Path.GetFileName(filePath)}!");
                Program.Log(e.ToString());
                return false;
            }

            return true;
        }

        private static void ConvertPath(ModsJson modsJson)
        {
            if (modsJson is null) throw new ArgumentNullException(nameof(modsJson));
            if (modsJson.FullPath is null) throw new ArgumentNullException(nameof(modsJson.FullPath), $"{modsJson.Name} has no FullPath!");
            PathData pathData = new(modsJson);
            ValidCheck validCheck = pathData.ValidCheck;
            if (validCheck != ValidCheck.Valid)
            {
                Program.Log($"Skipping {pathData}. Reason: {validCheck.ToString()}");
                return;
            }

            if (ReplaceDictionary.TryGetReplacementPath(pathData, out string replacementPath))
            {
                string prevPath = modsJson.FullPath;
                modsJson.FullPath = replacementPath;
                Program.Log($"Converted {modsJson.Name} from '{prevPath}' to '{modsJson.FullPath}'");
                return;
            }
            
            Program.Log($"Did not convert {pathData}.");
        }
    }
}