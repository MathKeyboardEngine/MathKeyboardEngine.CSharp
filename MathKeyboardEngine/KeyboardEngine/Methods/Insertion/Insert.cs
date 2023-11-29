namespace MathKeyboardEngine;

public static class __Insert
{
    public static void Insert(this KeyboardMemory k, IEnumerable<TreeNode> nodes)
    {
        foreach(var node in nodes)
        {
            k.InsertCore(node);
            k.Current = node;
        }
    }

    public static void Insert(this KeyboardMemory k, TreeNode newNode)
    {
        k.InsertCore(newNode);
        k.MoveRight();
    }

    private static void InsertCore(this KeyboardMemory k, TreeNode newNode)
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
    }
}
