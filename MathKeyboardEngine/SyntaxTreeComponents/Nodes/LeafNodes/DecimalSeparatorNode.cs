namespace MathKeyboardEngine;
public class DecimalSeparatorNode : PartOfNumberWithDigits
{
    private readonly Func<string> _latex;
    public DecimalSeparatorNode(string latex = ".")
    {
        _latex = () => latex;
    }
    public DecimalSeparatorNode(Func<string> latex)
    {
        _latex = latex;
    }

    protected override string GetLatexPart(KeyboardMemory k, LatexConfiguration latexConfiguration)
    {
        return _latex();
    }
}
