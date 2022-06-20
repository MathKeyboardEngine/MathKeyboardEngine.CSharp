

namespace MathKeyboardEngine;
public static class InsertWithEncapsulateSelectionMethod
{
    public static void InsertWithEncapsulateSelection(this KeyboardMemory k, BranchingNode newNode)
    {
        var selection = SelectionHelper.PopSelection(k);
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
