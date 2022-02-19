using System;
using TTMPLReplacer.Enums;

namespace TTMPLReplacer
{
    public static class Extensions
    {
        public static string GetShortTexType(this PathData pathData)
        {
            switch (pathData.TexType)
            {
                case TexType.Diffuse:
                    return "d";
                case TexType.Specular:
                    if (ReplacerForm.CorrectMatA && pathData.IsValid == ValidCheck.Gen2Tex)
                    {
                        return "s";
                    }
                    if (pathData.IsBiboConvert)
                    {
                        return "m";
                    }
                    else if (pathData.IsGen3Convert)
                    {
                        return "s";
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(nameof(pathData.SlotType), pathData.SlotType, "We don't know how to handle this SlotType! Check your dropdown settings.");
                    }
                case TexType.Normal:
                    return "n";
                default:
                    throw new ArgumentOutOfRangeException(nameof(pathData.TexType), pathData.TexType, $"Unknown TexType for {pathData}!");
            }
        }
    }
}