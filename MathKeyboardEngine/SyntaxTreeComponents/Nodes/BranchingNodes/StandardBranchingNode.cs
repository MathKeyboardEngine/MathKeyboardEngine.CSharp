namespace MathKeyboardEngine;

public class StandardBranchingNode(string before, string then, params string[] rest)
    : BranchingNode(Enumerable.Range(1, rest.Length + 1).Select(x => new Placeholder()).ToList())
{
    protected override string GetLatexPart(KeyboardMemory k, LatexConfiguration latexConfiguration)
    {
        var latex = before + Placeholders[0].GetLatex(k, latexConfiguration) + then;
        for (var i = 0; i < rest.Length; i++)
        {
            latex += Placeholders[i + 1].GetLatex(k, latexConfiguration) + rest[i];
        }
        return latex;
    }
}
