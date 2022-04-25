namespace MathKeyboardEngine;

public abstract class SyntaxTreeComponent
{
    public abstract string GetLatex(KeyboardMemory k, LatexConfiguration latexConfiguration);
}
