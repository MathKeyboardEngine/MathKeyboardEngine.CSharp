namespace MathKeyboardEngine;
public static class SelectionHelper
{
    public static void SetSelectionDiff(KeyboardMemory k, int diffWithCurrent)
    {
        k.SelectionDiff = diffWithCurrent;
        if (diffWithCurrent == 0)
        {
            k.InclusiveSelectionLeftBorder = null;
            k.InclusiveSelectionRightBorder = null;
        }
        else if (k.Current is Placeholder p)
        {
            k.InclusiveSelectionLeftBorder = p;
            k.InclusiveSelectionRightBorder = p.Nodes[diffWithCurrent - 1];
        }
        else
        {
            var current = ((TreeNode)k.Current);
            var nodes = current.ParentPlaceholder.Nodes;
            var indexOfCurrent = nodes.IndexOf(current);
            if (diffWithCurrent > 0)
            {
                k.InclusiveSelectionLeftBorder = nodes[indexOfCurrent + 1];
                k.InclusiveSelectionRightBorder = nodes[indexOfCurrent + diffWithCurrent];
            }
            else
            {
                var indexOfNewInclusiveSelectionLeftBorder = indexOfCurrent + diffWithCurrent + 1;
                if (indexOfNewInclusiveSelectionLeftBorder < 0)
                {
                    throw new Exception($"The {nameof(TreeNode)} at index 0 of the current {nameof(Placeholder)} is as far as you can go left if current is a {nameof(TreeNode)}.");
                }
                k.InclusiveSelectionLeftBorder = nodes[indexOfNewInclusiveSelectionLeftBorder];
                k.InclusiveSelectionRightBorder = current;
            }
        }
    }

    public static List<TreeNode> PopSelection(KeyboardMemory k)
    {
        if (k.SelectionDiff == null)
        {
            throw new Exception("Enter selection mode before calling this method.");
        }
        if (k.SelectionDiff == 0)
        {
            k.LeaveSelectionMode();
            return new();
        }
        var diff = k.SelectionDiff.Value;
        if (k.Current is Placeholder p)
        {
            k.LeaveSelectionMode();
            var selectedNodes = p.Nodes.GetRange(0, diff);
            p.Nodes.RemoveRange(0, diff);
            return selectedNodes;
        }
        else
        {
            var current = ((TreeNode)k.Current);
            var siblings = current.ParentPlaceholder.Nodes;
            var indexOfLeftBorder = siblings.IndexOf((TreeNode)k.InclusiveSelectionLeftBorder!);
            k.Current = siblings.FirstBeforeOrDefault(k.InclusiveSelectionLeftBorder!) ?? current.ParentPlaceholder;
            k.LeaveSelectionMode();
            var absDiff = Math.Abs(diff);
            var selectedNodes = siblings.GetRange(indexOfLeftBorder, absDiff);
            siblings.RemoveRange(indexOfLeftBorder, absDiff);
            return selectedNodes;
        }
    }
}
