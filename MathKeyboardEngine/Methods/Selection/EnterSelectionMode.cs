namespace MathKeyboardEngine;
public static class EnterSelectionModeMethod
{
    public static void EnterSelectionMode(this KeyboardMemory k)
    {
        SelectionHelper.SetSelectionDiff(k, 0);
    }
}
