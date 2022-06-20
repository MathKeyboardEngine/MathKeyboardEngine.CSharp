namespace MathKeyboardEngine;
public static class DeleteSelectionMethod
{
    public static void DeleteSelection(this KeyboardMemory k)
    {
        _ = SelectionHelper.PopSelection(k);
    }
}
