namespace MathKeyboardEngine._Helpers;
public static class _Encapsulate
{
    public static void Encapsulate(this Placeholder encapsulatingPlaceholder, IEnumerable<TreeNode> nodes)
    {
        foreach (var node in nodes)
        {
            node.ParentPlaceholder = encapsulatingPlaceholder;
            encapsulatingPlaceholder.Nodes.Add(node);
        }
    }
}
