namespace MathKeyboardEngine;

public static class _Insert
{
    public static void Insert(this KeyboardMemory k, TreeNode newNode)
    {
        if (k.Current is Placeholder)
        {
            var current = (Placeholder)k.Current;
            current.Nodes.Insert(0, newNode);
            newNode.ParentPlaceholder = current;
        }
        else
        {
            var current = ((TreeNode)k.Current);
            var parent = current.ParentPlaceholder;
            var indexOfCurrent = parent.Nodes.IndexOf(current);
            parent.Nodes.Insert(indexOfCurrent + 1, newNode);
            newNode.ParentPlaceholder = parent;
        }
        k.MoveRight();
    }
}
