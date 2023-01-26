namespace MathKeyboardEngine;
public class StandardLeafNode : LeafNode
{
    private readonly Func<string> _latex;
    public StandardLeafNode(string latex)
    {
        _latex = () => latex;
    }
    public StandardLeafNode(Func<string> latex)
    {
        _latex = latex;
    }

    protected override string GetLatexPart(KeyboardMemory k, LatexConfiguration latexConfiguration)
    {
        return _latex();
    }
}
