namespace MathKeyboardEngine;

public static class MethodHelpers
{
    public static void Encapsulate(this Placeholder encapsulatingPlaceholder, IEnumerable<TreeNode> nodes)
    {
        foreach (var node in nodes)
        {
            node.ParentPlaceholder = encapsulatingPlaceholder;
            encapsulatingPlaceholder.Nodes.Add(node);
        }
    }

    public static void EncapsulateAllPartsOfNumberWithDigitsLeftOfIndex(this Placeholder encapsulatingPlaceholder, int exclusiveRightIndex, List<TreeNode> siblingNodes)
    {
        for (var i = exclusiveRightIndex - 1; i >= 0; i--)
        {
            var siblingNode = siblingNodes[i];
            if (siblingNode is PartOfNumberWithDigits)
            {
                siblingNodes.Remove(siblingNode);
                encapsulatingPlaceholder.Nodes.Insert(0, siblingNode);
                siblingNode.ParentPlaceholder = encapsulatingPlaceholder;
            }
            else
            {
                break;
            }
        }
    }

    public static void EncapsulateAllPartsOfNumberWithDigitsLeftOfIndex(int exclusiveRightIndex, List<TreeNode> siblingNodes, Placeholder toPlaceholder)
    {
        for (var i = exclusiveRightIndex - 1; i >= 0; i--)
        {
            var siblingNode = siblingNodes[i];
            if (siblingNode is PartOfNumberWithDigits)
            {
                siblingNodes.Remove(siblingNode);
                toPlaceholder.Nodes.Insert(0, siblingNode);
                siblingNode.ParentPlaceholder = toPlaceholder;
            }
            else
            {
                break;
            }
        }
    }

    public static Placeholder? GetFirstNonEmptyOnLeftOf(this IEnumerable<Placeholder> source, Placeholder element)
    {
        return source.Reverse().SkipWhile(x => x != element).Skip(1).FirstOrDefault(x => x.Nodes.Count > 0);
    }
}
