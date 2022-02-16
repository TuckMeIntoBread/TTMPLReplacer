using System;
using System.Collections.Generic;
using TTMPLReplacer.Enums;

namespace TTMPLReplacer
{
    public static class ReplaceDictionary
    {
        public static bool TryGetReplacementPath(PathData pathData, out string replacementPath)
        {
            replacementPath = string.Empty;
            try
            {
                switch (Program.ConvertType)
                {
                    case ConvertType.Bibo:
                        return TryGetReplacementPathBibo(pathData, out replacementPath);
                    case ConvertType.Gen3:
                        return TryGetReplacementPathTitan(pathData, out replacementPath);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Program.ConvertType));
                }
            }
            catch (KeyNotFoundException e)
            {
                Program.Log($"Couldn't find the Race or Type code for {pathData}! Race: {pathData.RaceCode}, Type: {pathData.TypeCode}");
                Program.Log(e.ToString());
                return false;
            }
        }
        
        public static readonly HashSet<int> ValidRaceCodes = new()
        {
            // Hyur Midlander
            0201,
            // Hyur Highlander
            0401,
            // Au Ra
            1401,
            // Viera
            1801,
        };
        
        public static readonly HashSet<int> ValidTypeCodes = new()
        {
            // Type 1
            0001,
            // Type 1 Elder?
            0091,
            // Type 2
            0101,
            // Type 2 Elder?
            0191,
        };

        private static bool TryGetReplacementPathBibo(PathData pathData, out string replacementPath)
        {
            replacementPath = string.Empty;

            switch (pathData.RaceCode)
            {
                // Hyur Midlander
                case 0201:
                    replacementPath = BiboMidlanderDic[pathData.TypeCode];
                    break;
                // Hyur Highlander
                case 0401:
                    replacementPath = BiboHighlanderDic[pathData.TypeCode];
                    break;
                // Au Ra
                case 1401 when !pathData.HasPubes:
                    replacementPath = BiboAuRaDic[pathData.TypeCode];
                    break;
                // Au Ra with Pubes (fuck ur naming convention bizu)
                case 1401 when pathData.HasPubes:
                    replacementPath = BiboAuRaPubes[pathData.TypeCode];
                    break;
                // Viera
                case 1801:
                    replacementPath = BiboVieraDic[pathData.TypeCode];
                    break;
                default:
                    throw new KeyNotFoundException("Unknown/Invalid race code.");
            }

            if (pathData.RaceCode != 1401 && pathData.HasPubes) replacementPath = $"{replacementPath}_pubes";
            return true;
        }

        private static readonly Dictionary<int, string> BiboMidlanderDic = new()
        {
            { 0091, "elder_midlander" },
            { 0001, "midlander" },
        };
        
        private static readonly Dictionary<int, string> BiboHighlanderDic = new()
        {
            { 0001, "highlander" },
        };

        private static readonly Dictionary<int, string> BiboAuRaDic = new()
        {
            { 0191, "elder_xaela" },
            { 0101, "xaela" },
            { 0091, "elder_raen" },
            { 0001, "raen" },
        };
        
        private static readonly Dictionary<int, string> BiboVieraDic = new()
        {
            { 0001, "viera" },
        };

        private static readonly Dictionary<int, string> BiboAuRaPubes = new()
        {
            { 0191, "pubes/aura_elder_x_pubes" },
            { 0101, "pubes/aura_x_pubes" },
            { 0091, "pubes/aura_elder_r_pubes" },
            { 0001, "pubes/aura_r_pubes" },
        };
        
        private static bool TryGetReplacementPathTitan(PathData pathData, out string replacementPath)
        {
            replacementPath = string.Empty;

            if (pathData.HasPubes)
            {
                // TODO: Titan pube weirdness
                return false;
            }

            switch (pathData.RaceCode)
            {
                // Hyur Midlander
                case 0201:
                    replacementPath = TitanMidlanderDic[pathData.TypeCode];
                    break;
                // Hyur Highlander
                case 0401:
                    replacementPath = TitanHighlanderDic[pathData.TypeCode];
                    break;
                // Au Ra
                case 1401:
                    replacementPath = TitanAuRaDic[pathData.TypeCode];
                    break;
                // Viera
                case 1801:
                    replacementPath = TitanVieraDic[pathData.TypeCode];
                    break;
                default:
                    throw new KeyNotFoundException("Unknown/Invalid race code.");
            }

            return true;
        }

        private static readonly Dictionary<int, string> TitanMidlanderDic = new()
        {
            { 0001, "tfgen3midf" },
        };
        
        private static readonly Dictionary<int, string> TitanHighlanderDic = new()
        {
            { 0001, "tfgen3highf" },
        };

        private static readonly Dictionary<int, string> TitanAuRaDic = new()
        {
            { 0101, "tfgen3xaelaf" },
            { 0001, "tfgen3raenf" },
        };
        
        private static readonly Dictionary<int, string> TitanVieraDic = new()
        {
            { 0001, "tfgen3vieraf" },
        };
    }
}