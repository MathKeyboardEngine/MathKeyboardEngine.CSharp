﻿namespace MathKeyboardEngine
{
    public static class MoveDownMethod
    {
        public static void MoveDown(this KeyboardMemory k)
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
                Placeholder? suggestion = suggestingNode.GetMoveDownSuggestion(fromPlaceholder);
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