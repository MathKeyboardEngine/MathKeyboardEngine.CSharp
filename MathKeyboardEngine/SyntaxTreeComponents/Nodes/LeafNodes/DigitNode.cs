namespace MathKeyboardEngine;

public class DigitNode(string latex) : PartOfNumberWithDigits
{
    protected override string GetLatexPart(KeyboardMemory k, LatexConfiguration latexConfiguration)
    {
        return latex;
    }
}
