﻿namespace MathKeyboardEngine;

public class LatexParserConfiguration
{
    public List<string>? AdditionalDigits { get; set; }
    public List<string> DecimalSeparatorMatchers { get; set; } = [".", "{,}"];
    public Func<string>? PreferredDecimalSeparator { get; set; }
    public bool PreferRoundBracketsNode { get; set; } = true;
}
