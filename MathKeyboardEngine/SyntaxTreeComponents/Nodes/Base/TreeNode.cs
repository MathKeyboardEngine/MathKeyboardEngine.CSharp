using MathKeyboardEngine.__Helpers;

namespace MathKeyboardEngine;

public abstract class TreeNode : SyntaxTreeComponent
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value (...).
    public Placeholder ParentPlaceholder { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value (...).

    protected abstract string GetLatexPart(KeyboardMemory k, LatexConfiguration latexConfiguration);
    public override string GetLatex(KeyboardMemory k, LatexConfiguration latexConfiguration)
    {
        var latex = GetLatexPart(k, latexConfiguration);
        if (k.SelectionDiff != null && k.SelectionDiff != 0)
        {
            if (this == k.InclusiveSelectionLeftBorder)
            {
                latex = Concat.Latex(latexConfiguration.SelectionHightlightStart, latex);
            }
            if (this == k.InclusiveSelectionRightBorder)
            {
                latex = Concat.Latex(latex, latexConfiguration.SelectionHightlightEnd);
            }
            return latex;
        }
        else
        {
            if (this == k.Current)
            {
                return Concat.Latex(latex, latexConfiguration.ActivePlaceholderLatex);
            }
            else
            {
                return latex;
            }
        }
    }
}
