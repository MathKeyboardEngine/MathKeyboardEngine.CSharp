using MathKeyboardEngine._Helpers;
namespace MathKeyboardEngine;
public class Placeholder : SyntaxTreeComponent
{
    public BranchingNode? ParentNode { get; set; }
    public List<TreeNode> Nodes { get; } = new();

    public override string GetLatex(KeyboardMemory k, LatexConfiguration latexConfiguration)
    {
        if (this == k.InclusiveSelectionLeftBorder)
        {
            return Nodes.Select(x => x.GetLatex(k, latexConfiguration))
                .Prepend(latexConfiguration.SelectionHightlightStart)
                .ConcatLatex();
        }
        else if (this == k.Current)
        {
            if (Nodes.Count == 0)
            {
                return latexConfiguration.ActivePlaceholderLatex;
            }
            else
            {
                return Nodes.Select(x => x.GetLatex(k, latexConfiguration))
                .Prepend(latexConfiguration.ActivePlaceholderLatex)
                .ConcatLatex();
            }
        }
        else if (Nodes.Count == 0)
        {
            return latexConfiguration.PassivePlaceholderLatex;
        }
        else
        {
            return Nodes.Select(x => x.GetLatex(k, latexConfiguration)).ConcatLatex();
        }
    }
}
