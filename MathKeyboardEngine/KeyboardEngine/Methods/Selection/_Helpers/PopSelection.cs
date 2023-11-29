namespace MathKeyboardEngine.__Helpers;

public static class __PopSelection
{
    public static List<TreeNode> PopSelection(this KeyboardMemory k)
    {
        if (k.SelectionDiff == null)
        {
            throw new Exception("Enter selection mode before calling this method.");
        }
        if (k.SelectionDiff == 0)
        {
            k.LeaveSelectionMode();
            return [];
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
            var current = (TreeNode)k.Current;
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
