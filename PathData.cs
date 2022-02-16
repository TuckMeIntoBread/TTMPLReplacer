using System;
using System.Text.RegularExpressions;
using TTMPLReplacer.Enums;

namespace TTMPLReplacer
{
    public class PathData
    {
        public string Name { get; }

        public string FullPath { get; } = string.Empty;

        public int RaceCode { get; }

        public int TypeCode { get; }

        public string TexType { get; } = string.Empty;

        public bool HasPubes => FullPath.Contains("_pubes", StringComparison.OrdinalIgnoreCase);

        private readonly bool _isValid;

        public ValidCheck ValidCheck
        {
            get
            {
                if (!_isValid) return ValidCheck.RegexMismatch;
                if (string.IsNullOrEmpty(FullPath)) return ValidCheck.NoFullPath;
                if (string.IsNullOrEmpty(TexType)) return ValidCheck.NoTexType;
                if (!ReplaceDictionary.ValidRaceCodes.Contains(RaceCode)) return ValidCheck.InvalidRace;
                if (!ReplaceDictionary.ValidTypeCodes.Contains(TypeCode)) return ValidCheck.InvalidType;
                return ValidCheck.Valid;
            }
        }

        public PathData(ModsJson modsJson)
        {
            Name = modsJson.Name;
            Match regexMatch = ParsePath.Match(modsJson.FullPath);
            if (regexMatch.Success)
            {
                FullPath = modsJson.FullPath;
                _isValid = true;
                if (int.TryParse(regexMatch.Groups["race"].Value, out int raceNum)) RaceCode = raceNum;
                if (int.TryParse(regexMatch.Groups["fullType"].Value, out int typeNum)) TypeCode = typeNum;
                switch (regexMatch.Groups["end"].Value)
                {
                    case "dif":
                    case "diffuse":
                    case "d":
                        TexType = "d";
                        break;
                    case "mask":
                    case "multi":
                    case "spec":
                    case "specular":
                    case "m":
                    case "s":
                        TexType = "m";
                        break;
                    case "normal":
                    case "norm":
                    case "nrm":
                    case "n":
                        TexType = "n";
                        break;
                }
            }
        }

        private static readonly Regex ParsePath = new(@"^chara\/human\/c(?<race>\d{4})\/obj\/body\/b(?<fullType>(?<raceType>\d{2})(?<subType>\d{2}))\/.+_b_(?<end>.+)\.tex$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public override string ToString() => $"{Name} : '{FullPath}'";
    }
}