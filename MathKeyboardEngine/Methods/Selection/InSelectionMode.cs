namespace MathKeyboardEngine;
public static class InSelectionModeMethod
{
    public static bool InSelectionMode(this KeyboardMemory k)
    {
        return k.SelectionDiff != null;
    }
}
