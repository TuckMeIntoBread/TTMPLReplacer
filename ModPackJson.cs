// xivModdingFramework
// Copyright © 2018 Rafael Gonzalez - All Rights Reserved
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace TTMPLReplacer
{
    [JsonObject(MemberSerialization.OptOut)]
    public class ModPackJson
    {
        /// <summary>
        /// Tex Tools Mod Pack Version
        /// </summary>
        public string TTMPVersion { get; set; }

        /// <summary>
        /// The name of the mod pack
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The mod pack author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The mod pack version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The description for the mod pack
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The modpack's weblink.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Minimum required version of the Framework to import this modpack file.
        /// The 1.0.0.0 here is purely a fallback default; it should be assigned to
        /// in the actual modpack generation.
        /// </summary>
        public string MinimumFrameworkVersion = "1.0.0.0";

        /// <summary>
        /// Generates a hash identifier from this mod's name and author information,
        /// in lowercase.  For identifying new versions of same mods, potentially.
        /// </summary>
        /// <returns></returns>
        public byte[] GetHash()
        {
            using (SHA256 sha = SHA256.Create())
            {
                var n = Name.ToLower();
                var a = Author.ToLower();
                var key = n + a;
                var keyBytes = Encoding.Unicode.GetBytes(key);
                var hash = sha.ComputeHash(keyBytes);
                return hash;
            }
        }

        /// <summary>
        /// A list of pages containing a list of mod groups for that particular page
        /// </summary>
        public List<ModPackPageJson> ModPackPages { get; set; }

        /// <summary>
        /// The list of mods for simple modpacks
        /// </summary>
        public List<ModsJson> SimpleModsList { get; set; }

        [JsonIgnore]
        public IEnumerable<ModsJson> AllModsJsons
        {
            get
            {
                if (ModPackPages is not null)
                {
                    foreach (ModPackPageJson page in ModPackPages)
                    {
                        if (page is null) continue;
                        foreach (ModGroupJson group in page.ModGroups)
                        {
                            if (group is null) continue;
                            foreach (ModOptionJson option in group.OptionList)
                            {
                                if (option is null) continue;
                                foreach (ModsJson modsJson in option.ModsJsons)
                                {
                                    if (string.IsNullOrEmpty(modsJson?.FullPath)) continue;
                                    yield return modsJson;
                                }
                            }
                        }
                    }
                }

                if (SimpleModsList is null) yield break;
                foreach (ModsJson modsJson in SimpleModsList)
                {
                    if (string.IsNullOrEmpty(modsJson?.FullPath)) continue;
                    yield return modsJson;
                }
            }
        }

        public void RemoveMaterials()
        {
            if (ModPackPages is not null)
            {
                foreach (ModPackPageJson page in ModPackPages)
                {
                    if (page is null) continue;
                    foreach (ModGroupJson group in page.ModGroups)
                    {
                        if (group is null) continue;
                        foreach (ModOptionJson option in group.OptionList)
                        {
                            if (option is null || option.ModsJsons.Count == 0) continue;
                            var optionArray = option.ModsJsons.ToArray();
                            foreach (ModsJson modsJson in optionArray)
                            {
                                if (SkinMaterial.IsMatch(modsJson.FullPath))
                                {
                                    Program.Log($"Removed Material {modsJson.Name} : '{modsJson.FullPath}'");
                                    option.ModsJsons.Remove(modsJson);
                                }
                            }
                        }
                    }
                }
            }

            if (SimpleModsList is null || SimpleModsList.Count == 0) return;
            var simpleArray = SimpleModsList.ToArray();
            foreach (ModsJson modsJson in simpleArray)
            {
                if (SkinMaterial.IsMatch(modsJson.FullPath))
                {
                    Program.Log($"Removed Material {modsJson.Name} : '{modsJson.FullPath}'");
                    SimpleModsList.Remove(modsJson);
                }
            }
        }

        public static readonly Regex SkinMaterial = new(@"^chara\/human\/c\d{4}\/obj\/body\/b\d{4}\/material\/[^.]+\.mtrl$",
            RegexOptions.Compiled | RegexOptions.Multiline);
    }

    public class ModPackPageJson
    {
        /// <summary>
        /// The page index
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// The list of mod groups contained in the mod pack
        /// </summary>
        public List<ModGroupJson> ModGroups { get; set; }
    }

    public class ModGroupJson
    {
        /// <summary>
        /// The name of the mod options group
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// The type of selection for the options in the group
        /// </summary>
        /// <remarks>
        /// This is either Single Selection or Multi Selection
        /// </remarks>
        public string SelectionType { get; set; }

        /// <summary>
        /// The list of options in the group
        /// </summary>
        public List<ModOptionJson> OptionList { get; set; }
    }

    public class ModOptionJson : INotifyPropertyChanged
    {
        /// <summary>
        /// The name of the option
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The option description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The preview image path for the option
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// The list of mods in this option
        /// </summary>
        public List<ModsJson> ModsJsons { get; set; }

        /// <summary>
        /// The name of the group this mod option belongs to
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// The selection type for this mod option
        /// </summary>
        public string SelectionType { get; set; }

        /// <summary>
        /// The status of the radio or checkbox
        /// </summary>
        private bool isChecked;
        public bool IsChecked {
            get {
                return isChecked;
            }
            set {
                isChecked = value;
                NotifyPropertyChanged("IsChecked");
            } 
        }

        /// <summary>
        /// Implementation of INotifyPropertyChanged (to update UI)
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Helper to raise the property changed event
        /// </summary>
        /// <param name="propName">Name of the property that changed</param>
        private void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }

    public class ModsJson
    {
        /// <summary>
        /// The name of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The category of the item
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The full path of the item data for the game
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The offset to where the mod data is located
        /// </summary>
        public long ModOffset { get; set; }

        /// <summary>
        /// The size of the mod data
        /// </summary>
        public int ModSize { get; set; }

        /// <summary>
        /// The dat file associated with the item
        /// </summary>
        public string DatFile { get; set; }

        /// <summary>
        /// Indicates if this "Mod" is actually just a placeholder in a modpack, 
        /// indicating that we want any existing mod on this file to be disabled.
        /// 
        /// Default mod entries include completely valid copies of their original
        /// SE item, for backwards compatability and in case some code imports
        /// them incorrectly or needs the information for calculations.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// The Mod Pack Entry
        /// </summary>
        public ModPack ModPackEntry { get; set; }
    }
    
    public class ModPack
    {

        /// <summary>
        /// Generates a hash identifier from this mod's name and author information,
        /// in lowercase.  For identifying new versions of same mods, potentially.
        /// </summary>
        /// <returns></returns>
        public byte[] GetHash()
        {
            using (SHA256 sha = SHA256.Create())
            {
                var n = name.ToLower();
                var a = author.ToLower();
                var key = n + a;
                var keyBytes= Encoding.Unicode.GetBytes(key);
                var hash = sha.ComputeHash(keyBytes);
                return hash;
            }
        }

        /// <summary>
        /// The name of the modpack
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The modpack author
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// The modpack version
        /// </summary>
        public string version { get; set; }

        /// <summary>
        /// The URL the author associated with this modpack.
        /// </summary>
        public string url { get; set; }
    }
}