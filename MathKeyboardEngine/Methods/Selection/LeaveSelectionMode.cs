namespace MathKeyboardEngine;
public static class LeaveSelectionModeMethod
{
    public static void LeaveSelectionMode(this KeyboardMemory k)
    {
        k.SelectionDiff = null;
        k.InclusiveSelectionLeftBorder = null;
        k.InclusiveSelectionRightBorder = null;
    }
}
