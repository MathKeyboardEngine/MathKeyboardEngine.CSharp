namespace MathKeyboardEngine;

public static class _GetEditModeLatex
{
    public static string GetEditModeLatex(this KeyboardMemory k, LatexConfiguration latexConfiguration)
    {
        return k.SyntaxTreeRoot.GetLatex(k, latexConfiguration);
    }
}
