using MathKeyboardEngine.__Helpers;

namespace MathKeyboardEngine;

public static class __MoveLeft
{
    public static void MoveLeft(this KeyboardMemory k)
    {
        if (k.Current is Placeholder)
        {
            var current = (Placeholder)k.Current;
            if (current.ParentNode == null)
            {
                return;
            }

            var previousPlaceholder = current.ParentNode.Placeholders.FirstBeforeOrDefault(current);
            if (previousPlaceholder != null)
            {
                k.Current = previousPlaceholder.Nodes.LastOrDefault() ?? (SyntaxTreeComponent)previousPlaceholder;
            }
            else
            {
                var ancestorPlaceholder = current.ParentNode.ParentPlaceholder;
                var nodePreviousToParentOfCurrent = ancestorPlaceholder.Nodes.FirstBeforeOrDefault(current.ParentNode);
                k.Current = nodePreviousToParentOfCurrent ?? (SyntaxTreeComponent)ancestorPlaceholder;
            }
        }
        else if (k.Current is BranchingNode)
        {
            var current = (BranchingNode)k.Current;
            var placeholder = current.Placeholders.Last();
            k.Current = placeholder.Nodes.LastOrDefault() ?? (SyntaxTreeComponent)placeholder;
        }
        else
        {
            var current = ((LeafNode)k.Current);
            k.Current = current.ParentPlaceholder.Nodes.FirstBeforeOrDefault(current)
                ?? (SyntaxTreeComponent)current.ParentPlaceholder;
        }
    }
}
