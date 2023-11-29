using MathKeyboardEngine.__Helpers;

namespace MathKeyboardEngine;

public static class __EnterSelectionMode
{
    public static void EnterSelectionMode(this KeyboardMemory k)
    {
        k.SetSelectionDiff(0);
    }
}
