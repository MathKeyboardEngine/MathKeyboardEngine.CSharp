namespace MathKeyboardEngine._Helpers;

public static class _EncapsulateAllPartsOfNumberWithDigitsLeftOfIndex
{
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
}
