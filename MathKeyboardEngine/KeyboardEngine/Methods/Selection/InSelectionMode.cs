namespace MathKeyboardEngine;
public static class _InSelectionMode
{
    public static bool InSelectionMode(this KeyboardMemory k)
    {
        return k.SelectionDiff != null;
    }
}
