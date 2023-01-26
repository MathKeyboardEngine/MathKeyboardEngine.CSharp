using MathKeyboardEngine._Helpers;
namespace MathKeyboardEngine;
public static class _SelectLeft
{
    public static void SelectLeft(this KeyboardMemory k)
    {
        var oldDiffWithCurrent = k.SelectionDiff ?? 0;
        if ((k.Current is TreeNode current && current.ParentPlaceholder.Nodes.IndexOf(current) + oldDiffWithCurrent >= 0) || (k.Current is Placeholder && oldDiffWithCurrent > 0))
        {
            k.SetSelectionDiff(oldDiffWithCurrent - 1);
        }
        else if (
            k.InclusiveSelectionLeftBorder is TreeNode leftBorder &&
            leftBorder.ParentPlaceholder.Nodes.IndexOf(leftBorder) == 0 &&
            leftBorder.ParentPlaceholder.ParentNode != null)
        {
            k.Current = leftBorder.ParentPlaceholder.ParentNode;
            k.SetSelectionDiff(-1);
        }
    }
}
