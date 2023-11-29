using MathKeyboardEngine.__Helpers;

namespace MathKeyboardEngine;

public static class __DeleteSelection
{
    public static void DeleteSelection(this KeyboardMemory k)
    {
        _ = k.PopSelection();
    }
}
