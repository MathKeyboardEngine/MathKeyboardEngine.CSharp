namespace MathKeyboardEngine
{
    public static class MoveLeftMethod
    {
        public static void MoveLeft(KeyboardMemory k)
        {
            if (k.Current is Placeholder)
            {
                Placeholder? current = (Placeholder)k.Current;
                if (current.ParentNode == null)
                {
                    return;
                }

                Placeholder? previousPlaceholder = current.ParentNode.Placeholders.FirstBeforeOrDefault(current);
                if (previousPlaceholder != null)
                {
                    k.Current = previousPlaceholder.Nodes.LastOrDefault() ?? (SyntaxTreeComponent)previousPlaceholder;
                }
                else
                {
                    Placeholder? ancestorPlaceholder = current.ParentNode.ParentPlaceholder;
                    TreeNode? nodePreviousToParentOfCurrent = ancestorPlaceholder.Nodes.FirstBeforeOrDefault(current.ParentNode);
                    k.Current = nodePreviousToParentOfCurrent ?? (SyntaxTreeComponent)ancestorPlaceholder;
                }
            }
            else if (k.Current is BranchingNode)
            {
                BranchingNode? current = (BranchingNode)k.Current;
                k.Current = current.Placeholders.SelectMany(x => x.Nodes).LastOrDefault()
                    ?? (SyntaxTreeComponent)current.Placeholders.Last();
            }
            else
            {
                LeafNode? current = ((LeafNode)k.Current);
                k.Current = current.ParentPlaceholder.Nodes.FirstBeforeOrDefault(current)
                    ?? (SyntaxTreeComponent)current.ParentPlaceholder;
            }
        }
    }
}
