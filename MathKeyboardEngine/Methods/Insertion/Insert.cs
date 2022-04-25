namespace MathKeyboardEngine;

public static class InsertMethod
{
    public static void Insert(this KeyboardMemory k, TreeNode newNode)
    {
        if (k.Current is Placeholder current)
        {
            current.Nodes.Insert(0, newNode);
            newNode.ParentPlaceholder = current;
        }
        else
        {
            var parent = ((TreeNode)k.Current).ParentPlaceholder;
            var indexOfCurrent = parent.Nodes.IndexOf(newNode);
            parent.Nodes.Insert(indexOfCurrent + 1, newNode);
            newNode.ParentPlaceholder = parent;
        }
        k.MoveRight();
    }
}
