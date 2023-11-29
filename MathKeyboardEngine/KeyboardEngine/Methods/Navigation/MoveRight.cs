using MathKeyboardEngine.__Helpers;

namespace MathKeyboardEngine;

public static class __MoveRight
{
    public static void MoveRight(this KeyboardMemory k)
    {
        if (k.Current is Placeholder)
        {
            var current = (Placeholder)k.Current;
            if (current.Nodes.Count > 0)
            {
                var nextNode = current.Nodes[0];
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
            var current = ((TreeNode)k.Current);
            var nextNode = current.ParentPlaceholder.Nodes.FirstAfterOrDefault(current);
            if (nextNode != null)
            {
                k.Current = nextNode is LeafNode ? nextNode : ((BranchingNode)nextNode).Placeholders[0];
            }
            else
            {
                var ancestorNode = current.ParentPlaceholder.ParentNode;
                if (ancestorNode != null)
                {
                    var nextPlaceholder = ancestorNode.Placeholders.FirstAfterOrDefault(current.ParentPlaceholder);
                    k.Current = nextPlaceholder ?? (SyntaxTreeComponent)ancestorNode;
                }
            }
        }
    }
}
