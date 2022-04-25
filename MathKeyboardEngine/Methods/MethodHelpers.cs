namespace MathKeyboardEngine
{
    public static class MethodHelpers
    {
        public static void Encapsulate(this Placeholder encapsulatingPlaceholder, IEnumerable<TreeNode> nodes)
        {
            foreach (TreeNode? node in nodes)
            {
                node.ParentPlaceholder = encapsulatingPlaceholder;
                encapsulatingPlaceholder.Nodes.Add(node);
            }
        }

        public static void EncapsulateAllPartsOfNumberWithDigitsLeftOfIndex(this Placeholder encapsulatingPlaceholder, int exclusiveRightIndex, List<TreeNode> siblingNodes)
        {
            for (int i = exclusiveRightIndex - 1; i >= 0; i--)
            {
                TreeNode siblingNode = siblingNodes[i];
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
}
