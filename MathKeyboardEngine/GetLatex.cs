namespace MathKeyboardEngine;

public static class GetLatex
{
    private static readonly KeyboardMemory _emptyKeyboardMemory = new();
    public static string GetEditModeLatex(this KeyboardMemory k, LatexConfiguration latexConfiguration)
    {
        return k.SyntaxTreeRoot.GetLatex(k, latexConfiguration);
    }

    public static string GetViewModeLatex(this KeyboardMemory k, LatexConfiguration latexConfiguration)
    {
        return GetViewModeLatex(k.SyntaxTreeRoot, latexConfiguration);
    }

    public static string GetViewModeLatex(this SyntaxTreeComponent syntaxTreeComponent, LatexConfiguration latexConfiguration)
    {
        return syntaxTreeComponent.GetLatex(_emptyKeyboardMemory, latexConfiguration);
    }

}
