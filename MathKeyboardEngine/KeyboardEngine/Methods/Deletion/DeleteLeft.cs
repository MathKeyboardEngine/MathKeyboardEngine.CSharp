using MathKeyboardEngine._Helpers;
namespace MathKeyboardEngine;
public static class _DeleteLeft
{
    public static void DeleteLeft(this KeyboardMemory k)
    {
        if (k.Current is Placeholder)
        {
            var current = (Placeholder)k.Current;
            if (current.ParentNode == null || current.Nodes.Count > 0)
            {
                return;
            }
            else
            {
                var nonEmptyPlaceholderOnLeft = current.ParentNode.Placeholders.GetFirstNonEmptyOnLeftOf(current);
                if (nonEmptyPlaceholderOnLeft != null)
                {
                    if (current.ParentNode.Placeholders.Count == 2 && current == current.ParentNode.Placeholders[1] && current.Nodes.Count == 0)
                    {
                        k.DeleteOuterBranchingNodeButNotItsContents(nonEmptyPlaceholderOnLeft);
                    }
                    else
                    {
                        nonEmptyPlaceholderOnLeft.Nodes.RemoveAt(nonEmptyPlaceholderOnLeft.Nodes.Count - 1);
                        k.Current = nonEmptyPlaceholderOnLeft.Nodes.LastOrDefault() ?? (SyntaxTreeComponent)nonEmptyPlaceholderOnLeft;
                    }
                }
                else if (current.ParentNode.Placeholders.All(x => x.Nodes.Count == 0))
                {
                    var ancestorPlaceholder = current.ParentNode.ParentPlaceholder;
                    var previousNode = ancestorPlaceholder.Nodes.FirstBeforeOrDefault(current.ParentNode);
                    ancestorPlaceholder.Nodes.Remove(current.ParentNode);
                    k.Current = previousNode ?? (SyntaxTreeComponent)ancestorPlaceholder;
                }
                else if (current.ParentNode.Placeholders[0] == current && current.Nodes.Count == 0 && current.ParentNode.Placeholders.Any(x => x.Nodes.Count > 0))
                {
                    var previousNode = current.ParentNode!.ParentPlaceholder.Nodes.FirstBeforeOrDefault(current.ParentNode);
                    if (previousNode != null)
                    {
                        EncapsulatePreviousInto(previousNode, current);
                        k.Current = current.Nodes.Last();
                    }
                    else
                    {
                        var nonEmptySiblingPlaceholders = current.ParentNode.Placeholders.Where(x => x.Nodes.Count != 0).ToArray();
                        if (nonEmptySiblingPlaceholders.Length == 1)
                        {
                            var nodes = nonEmptySiblingPlaceholders[0].Nodes;
                            var ancestorPlaceholder = current.ParentNode.ParentPlaceholder;
                            var indexOfParentNode = ancestorPlaceholder.Nodes.IndexOf(current.ParentNode);
                            foreach (var node in nodes)
                            {
                                node.ParentPlaceholder = ancestorPlaceholder;
                            }
                            ancestorPlaceholder.Nodes.RemoveAt(indexOfParentNode);
                            ancestorPlaceholder.Nodes.InsertRange(indexOfParentNode, nodes);
                            k.Current = nodes.Last();
                        }
                    }
                }
            }
        }
        else
        {
            var current = ((TreeNode)k.Current);
            if (current is BranchingNode b && b.Placeholders[0].Nodes.Count > 0 && b.Placeholders.Skip(1).All(x => x.Nodes.Count == 0))
            {
                k.DeleteOuterBranchingNodeButNotItsContents(b.Placeholders[0]);
            }
            else if (current is BranchingNode b2 && b2.Placeholders.Any(x => x.Nodes.Count > 0))
            {
                k.Current = b2.Placeholders.SelectMany(x => x.Nodes).Last();
                k.DeleteLeft();
            }
            else
            {
                var previousNode = current.ParentPlaceholder.Nodes.FirstBeforeOrDefault(current);
                current.ParentPlaceholder.Nodes.Remove(current);
                k.Current = previousNode ?? (SyntaxTreeComponent)current.ParentPlaceholder;
            }
        }
    }

    private static void DeleteOuterBranchingNodeButNotItsContents(this KeyboardMemory k, Placeholder nonEmptyPlaceholder)
    {
        var outerBranchingNode = nonEmptyPlaceholder.ParentNode!;
        var indexOfOuterBranchingNode = outerBranchingNode.ParentPlaceholder.Nodes.IndexOf(outerBranchingNode);
        outerBranchingNode.ParentPlaceholder.Nodes.RemoveAt(indexOfOuterBranchingNode);
        outerBranchingNode.ParentPlaceholder.Nodes.InsertRange(indexOfOuterBranchingNode, nonEmptyPlaceholder.Nodes);
        foreach (var node in nonEmptyPlaceholder.Nodes)
        {
            node.ParentPlaceholder = outerBranchingNode.ParentPlaceholder;
        }
        k.Current = nonEmptyPlaceholder.Nodes.Last();
    }

    public static void EncapsulatePreviousInto(TreeNode previousNode, Placeholder targetPlaceholder)
    {
        targetPlaceholder.ParentNode!.ParentPlaceholder.Nodes.Remove(previousNode);
        targetPlaceholder.Nodes.Add(previousNode);
        var previousNodeOldParentPlaceholder = previousNode.ParentPlaceholder;
        previousNode.ParentPlaceholder = targetPlaceholder;
        if (previousNode is PartOfNumberWithDigits)
        {
            targetPlaceholder.EncapsulateAllPartsOfNumberWithDigitsLeftOfIndex(previousNodeOldParentPlaceholder.Nodes.Count - 1, previousNodeOldParentPlaceholder.Nodes);
        }
    }
}
