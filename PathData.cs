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

        public string FileMid { get; }
        
        public string FileSuffix { get; }
        
        public string Path { get; }

        public int RaceCode { get; }

        public int TypeCode { get; }

        public TexType TexType { get; } = TexType.None;

        public bool IsPubes => string.Equals(FileMid, "c", StringComparison.OrdinalIgnoreCase) || string.Equals(FileMid, "e", StringComparison.OrdinalIgnoreCase);
        
        public bool IsSkin => string.Equals(FileMid, "b", StringComparison.OrdinalIgnoreCase) || string.Equals(FileMid, "d", StringComparison.OrdinalIgnoreCase);

        private readonly bool _validPath;
        
        private readonly bool _validFile;

        private bool ValidRace => ReplaceDictionary.ValidRaceCodes.Contains(RaceCode);

        private bool ValidType => ReplaceDictionary.ValidTypeCodes.Contains(TypeCode);

        public ValidCheck ValidCheck
        {
            get
            {
                if (string.IsNullOrEmpty(FullPath)) return ValidCheck.NoFullPath;
                if (!_validPath) return ValidCheck.NotBodyPath;
                if (!_validFile) return ValidCheck.Gen2Tex;
                if (!ValidRace) return ValidCheck.InvalidRace;
                if (!ValidType) return ValidCheck.InvalidType;
                if (TexType == TexType.None) return ValidCheck.NoTexType;
                if (!IsPubes && !IsSkin) return ValidCheck.UnknownMid;
                return ValidCheck.Valid;
            }
        }

        public PathData(ModsJson modsJson)
        {
            Name = modsJson.Name;
            FullPath = modsJson.FullPath;
            Match fullMatch = ParsePath.Match(modsJson.FullPath);
            if (!fullMatch.Success) return;
            _validPath = true;
            if (int.TryParse(fullMatch.Groups["race"].Value, out int raceNum)) RaceCode = raceNum;
            if (int.TryParse(fullMatch.Groups["type"].Value, out int typeNum)) TypeCode = typeNum;
            if (!ValidRace || !ValidType) return;
            FileName = fullMatch.Groups["filename"].Value;
            Path = fullMatch.Groups["path"].Value;
            Match fileMatch = ParseFile.Match(FileName);
            if (!fileMatch.Success) return;
            _validFile = true;
            FileMid = fileMatch.Groups["mid"].Value.ToLower();
            FileSuffix = fileMatch.Groups["suffix"].Value.ToLower();
            switch (FileSuffix)
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
                    break;
            }
        }

        private static readonly Regex ParsePath = new(@"^(?<path>chara\/human\/c(?<race>\d{4})\/obj\/body\/b(?<type>\d{4})\/texture\/)(?<filename>[^.]+\.tex)$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex ParseFile = new(@"^(?:--)?(?:v\d{2}_)?c\d{4}[bf]\d{4}_(?<mid>[cbde])_(?<suffix>[^_.]+)\.tex$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public override string ToString() => $"{Name} : '{FullPath}'";
    }
}