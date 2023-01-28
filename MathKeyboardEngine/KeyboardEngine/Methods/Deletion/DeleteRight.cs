using MathKeyboardEngine._Helpers;

namespace MathKeyboardEngine;

public static class _DeleteRight
{
    public static void DeleteRight(this KeyboardMemory k)
    {
        if (k.Current is Placeholder current)
        {
            if (current.ParentNode != null && current.ParentNode.Placeholders.All(ph => ph.Nodes.Count == 0))
            {
                var previousNode = current.ParentNode.ParentPlaceholder.Nodes.FirstBeforeOrDefault(current.ParentNode);
                current.ParentNode.ParentPlaceholder.Nodes.Remove(current.ParentNode);
                k.Current = previousNode ?? (SyntaxTreeComponent)current.ParentNode.ParentPlaceholder;
            }
            else
            {
                var nodes = current.Nodes;
                if (nodes.Count > 0)
                {
                    HandleDeletion(k, nodes[0]);
                }
                else if (current.ParentNode != null)
                {
                    var parentNode = current.ParentNode;
                    var siblingPlaceholders = parentNode.Placeholders;
                    if (siblingPlaceholders[0] == current && siblingPlaceholders.Count == 2)
                    {
                        var nonEmptyPlaceholder = siblingPlaceholders[1];
                        k.Current = parentNode.ParentPlaceholder.Nodes.FirstBeforeOrDefault(parentNode) ?? (SyntaxTreeComponent)parentNode.ParentPlaceholder;
                        nonEmptyPlaceholder.DeleteOuterBranchingNodeButNotItsContents();
                    }
                    else
                    {
                        for (var i = siblingPlaceholders.IndexOf(current) + 1; i < siblingPlaceholders.Count; i++)
                        {
                            if (siblingPlaceholders[i].Nodes.Count > 0)
                            {
                                k.Current = siblingPlaceholders[i];
                                DeleteRight(k);
                                return;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            var nextNode = (TreeNode?)((TreeNode)k.Current).ParentPlaceholder.Nodes.FirstAfterOrDefault(k.Current);
            if (nextNode != null)
            {
                HandleDeletion(k, nextNode);
            }
        }
    }

    public static void HandleDeletion(KeyboardMemory k, TreeNode nextTreeNode)
    {
        if (nextTreeNode is BranchingNode nextNode)
        {
            if (nextNode.Placeholders.Count == 1 && nextNode.Placeholders[0].Nodes.Count > 0)
            {
                nextNode.Placeholders[0].DeleteOuterBranchingNodeButNotItsContents();
            }
            else if (nextNode.Placeholders.Count == 2 && nextNode.Placeholders[0].Nodes.Count == 0 && nextNode.Placeholders[1].Nodes.Count > 0)
            {
                nextNode.Placeholders[1].DeleteOuterBranchingNodeButNotItsContents();
            }
            else
            {
                k.Current = nextNode.Placeholders[0];
                DeleteRight(k);
            }
        }
        else
        {
            nextTreeNode.ParentPlaceholder.Nodes.Remove(nextTreeNode);
        }
    }
}
