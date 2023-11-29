namespace MathKeyboardEngine;

public static class __GetViewModeLatex
{
    private static readonly KeyboardMemory _emptyKeyboardMemory = new();
    public static string GetViewModeLatex(this SyntaxTreeComponent syntaxTreeComponent, LatexConfiguration latexConfiguration)
    {
        return syntaxTreeComponent.GetLatex(_emptyKeyboardMemory, latexConfiguration);
    }

    public static string GetViewModeLatex(this KeyboardMemory k, LatexConfiguration latexConfiguration)
    {
        return k.SyntaxTreeRoot.GetViewModeLatex(latexConfiguration);
    }
}
