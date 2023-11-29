namespace MathKeyboardEngine;

public static class __InSelectionMode
{
    public static bool InSelectionMode(this KeyboardMemory k)
    {
        return k.SelectionDiff != null;
    }
}
