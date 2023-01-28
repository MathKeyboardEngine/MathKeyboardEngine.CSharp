using MathKeyboardEngine._Helpers;

namespace MathKeyboardEngine;

public static class _SelectRight
{
    public static void SelectRight(this KeyboardMemory k)
    {
        var oldDiffWithCurrent = k.SelectionDiff ?? 0;
        if ((k.Current is Placeholder p && oldDiffWithCurrent < p.Nodes.Count) ||
            (k.Current is TreeNode t && t.ParentPlaceholder.Nodes.IndexOf(t) + oldDiffWithCurrent < t.ParentPlaceholder.Nodes.Count - 1))
        {
            k.SetSelectionDiff(oldDiffWithCurrent + 1);
        }
        else if (k.InclusiveSelectionRightBorder is TreeNode rightBorder
            && rightBorder.ParentPlaceholder.Nodes.LastOrDefault() == rightBorder
            && rightBorder.ParentPlaceholder.ParentNode != null)
        {
            var ancestorNode = rightBorder.ParentPlaceholder.ParentNode;
            k.Current = ancestorNode.ParentPlaceholder.Nodes.FirstBeforeOrDefault(ancestorNode) ?? (SyntaxTreeComponent)ancestorNode.ParentPlaceholder;
            k.SetSelectionDiff(1);
        }
    }
}
