namespace MathKeyboardEngine
{
    public static class MoveRightMethod
    {
        public static void MoveRight(this KeyboardMemory k)
        {
            if (k.Current is Placeholder)
            {
                Placeholder? current = (Placeholder)k.Current;
                if (current.Nodes.Count > 0)
                {
                    TreeNode? nextNode = current.Nodes[0];
                    k.Current = nextNode is LeafNode ? nextNode : ((BranchingNode)nextNode).Placeholders[0];
                }
                else if (current.ParentNode == null)
                {
                    return;
                }
                else
                {
                    k.Current = current.ParentNode.Placeholders.FirstAfterOrDefault(current) ?? (SyntaxTreeComponent)current.ParentNode;
                }
            }
            else
            {
                TreeNode? current = ((TreeNode)k.Current);
                TreeNode? nextNode = current.ParentPlaceholder.Nodes.FirstAfterOrDefault(current);
                if (nextNode != null)
                {
                    k.Current = nextNode is LeafNode ? nextNode : ((BranchingNode)nextNode).Placeholders[0];
                }
                else
                {
                    BranchingNode? ancestorNode = current.ParentPlaceholder.ParentNode;
                    if (ancestorNode != null)
                    {
                        Placeholder? nextPlaceholder = ancestorNode.Placeholders.FirstAfterOrDefault(current.ParentPlaceholder);
                        k.Current = nextPlaceholder ?? (SyntaxTreeComponent)ancestorNode;
                    }
                }
            }
        }
    }
}
