namespace MathKeyboardEngine;

public static class _MoveDown
{
    public static void MoveDown(this KeyboardMemory k)
    {
        var fromPlaceholder = k.Current is Placeholder p ? p : ((TreeNode)k.Current).ParentPlaceholder;
        BranchingNode suggestingNode;
        while (true)
        {
            if (fromPlaceholder.ParentNode == null)
            {
                return;
            }
            suggestingNode = fromPlaceholder.ParentNode;
            var suggestion = suggestingNode.GetMoveDownSuggestion(fromPlaceholder);
            if (suggestion != null)
            {
                k.Current = suggestion.Nodes.LastOrDefault() ?? (SyntaxTreeComponent)suggestion;
                return;
            }
            fromPlaceholder = suggestingNode.ParentPlaceholder;
        }
    }
}
