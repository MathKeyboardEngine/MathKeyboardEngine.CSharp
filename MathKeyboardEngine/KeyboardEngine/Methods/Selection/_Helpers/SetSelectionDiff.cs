namespace MathKeyboardEngine.__Helpers;

public static class __SetSelectionDiff
{
    public static void SetSelectionDiff(this KeyboardMemory k, int diffWithCurrent)
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
            var current = (TreeNode)k.Current;
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
}
