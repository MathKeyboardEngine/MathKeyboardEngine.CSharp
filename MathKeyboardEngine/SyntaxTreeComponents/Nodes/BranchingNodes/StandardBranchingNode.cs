namespace MathKeyboardEngine;

public class StandardBranchingNode : BranchingNode
{
    private readonly string _before;
    private readonly string _then;
    private readonly string[] _rest;
    public StandardBranchingNode(string before, string then, params string[] rest)
        : base(Enumerable.Range(1, rest.Length + 2).Select(x => new Placeholder()).ToList())
    {
        _before = before;
        _then = then;
        _rest = rest;
    }
    protected override string GetLatexPart(KeyboardMemory k, LatexConfiguration latexConfiguration)
    {
        var latex = _before + Placeholders[0].GetLatex(k, latexConfiguration) + _then;
        for (var i = 0; i < _rest.Length; i++)
        {
            latex += Placeholders[i + 1].GetLatex(k, latexConfiguration) + _rest[i];
        }
        return latex;
    }
}
