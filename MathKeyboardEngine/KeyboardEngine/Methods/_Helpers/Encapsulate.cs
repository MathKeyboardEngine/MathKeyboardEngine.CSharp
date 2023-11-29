namespace MathKeyboardEngine.__Helpers;

public static class __Encapsulate
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
