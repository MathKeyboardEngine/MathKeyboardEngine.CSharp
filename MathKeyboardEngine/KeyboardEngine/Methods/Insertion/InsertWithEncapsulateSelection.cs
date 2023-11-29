using MathKeyboardEngine.__Helpers;

namespace MathKeyboardEngine;

public static class __InsertWithEncapsulateSelection
{
    public static void InsertWithEncapsulateSelection(this KeyboardMemory k, BranchingNode newNode)
    {
        var selection = k.PopSelection();
        k.Insert(newNode);
        if (selection.Count > 0)
        {
            var encapsulatingPlaceholder = newNode.Placeholders[0];
            encapsulatingPlaceholder.Encapsulate(selection);
            k.Current = selection.Last();
            k.MoveRight();
        }
    }
}
