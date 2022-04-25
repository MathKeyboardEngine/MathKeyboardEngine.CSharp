namespace MathKeyboardEngine
{
    public static class MoveUpMethod
    {
        public static void MoveUp(this KeyboardMemory k)
        {
            Placeholder? fromPlaceholder = k.Current is Placeholder p ? p : ((TreeNode)k.Current).ParentPlaceholder;
            BranchingNode suggestingNode;
            while (true)
            {
                if (fromPlaceholder.ParentNode == null)
                {
                    return;
                }
                suggestingNode = fromPlaceholder.ParentNode;
                Placeholder? suggestion = suggestingNode.GetMoveUpSuggestion(fromPlaceholder);
                if (suggestion != null)
                {
                    k.Current = suggestion.Nodes.LastOrDefault() ?? (SyntaxTreeComponent)suggestion;
                    return;
                }
                fromPlaceholder = suggestingNode.ParentPlaceholder;
            }
        }
    }
}
