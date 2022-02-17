using System;
using System.Text.RegularExpressions;
using TTMPLReplacer.Enums;

namespace TTMPLReplacer
{
    public class PathData
    {
        public string Name { get; }

        public string FullPath { get; }
        
        public string FileName { get; }

        public string FileSlot { get; }

        public string FileTexType { get; }
        
        public string Path { get; }

        public int RaceCode { get; }

        public int TypeCode { get; }

        public TexType TexType { get; } = TexType.None;

        public bool IsPubes => string.Equals(FileSlot, "c", StringComparison.OrdinalIgnoreCase) || string.Equals(FileSlot, "e", StringComparison.OrdinalIgnoreCase);
        
        public bool IsSkin => string.Equals(FileSlot, "b", StringComparison.OrdinalIgnoreCase) || string.Equals(FileSlot, "d", StringComparison.OrdinalIgnoreCase);

        private bool ValidRace => ReplaceDictionary.ValidRaceCodes.Contains(RaceCode);

        private bool ValidType => ReplaceDictionary.ValidTypeCodes.Contains(TypeCode);

        public ValidCheck IsValid { get; } = ValidCheck.Unknown;

        public PathData(ModsJson modsJson)
        {
            Name = modsJson.Name;
            FullPath = modsJson.FullPath;
            Match fullMatch = ParseFullPath.Match(modsJson.FullPath);
            if (!fullMatch.Success)
            {
                IsValid = ValidCheck.FullPathRegexMismatch;
                return;
            }
            if (int.TryParse(fullMatch.Groups["race"].Value, out int raceNum)) RaceCode = raceNum;
            if (int.TryParse(fullMatch.Groups["type"].Value, out int typeNum)) TypeCode = typeNum;
            if (!ValidRace)
            {
                IsValid = ValidCheck.InvalidRace;
                return;
            }

            if (!ValidType)
            {
                IsValid = ValidCheck.InvalidType;
                return;
            }
            FileName = fullMatch.Groups["filename"].Value;
            Path = fullMatch.Groups["path"].Value;
            Match fileMatch = ParseFileName.Match(FileName);
            if (!fileMatch.Success)
            {
                IsValid = ValidCheck.Gen2Tex;
                return;
            }
            FileSlot = fileMatch.Groups["mid"].Value.ToLower();
            if (!IsPubes && !IsSkin)
            {
                IsValid = ValidCheck.InvalidSlot;
                return;
            }
            FileTexType = fileMatch.Groups["texType"].Value.ToLower();
            switch (FileTexType)
            {
                case "dif":
                case "diffuse":
                case "d":
                    TexType = TexType.Diffuse;
                    break;
                case "mask":
                case "multi":
                case "spec":
                case "specular":
                case "m":
                case "s":
                    TexType = TexType.Specular;
                    break;
                case "normal":
                case "norm":
                case "nrm":
                case "n":
                    TexType = TexType.Normal;
                    break;
                default:
                    TexType = TexType.None;
                    IsValid = ValidCheck.InvalidTexType;
                    break;
            }

            IsValid = ValidCheck.Valid;
        }

        private static readonly Regex ParseFullPath = new(@"^(?<path>chara\/human\/c(?<race>\d{4})\/obj\/body\/(?<type>b\d{4})\/texture\/)(?<filename>.+)$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex ParseFileName = new(@"^(?:--)?(?:v\d{2}_)?c\d{4}[bf]\d{4}_(?<mid>[cbde])_(?<texType>[^_.]+)\.tex$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public override string ToString() => $"{Name} : '{FullPath}'";
    }
}