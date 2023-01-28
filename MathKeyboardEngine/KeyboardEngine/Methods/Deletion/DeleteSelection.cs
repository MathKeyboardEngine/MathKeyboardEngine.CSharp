using MathKeyboardEngine._Helpers;

namespace MathKeyboardEngine;

public static class _DeleteSelection
{
    public static void DeleteSelection(this KeyboardMemory k)
    {
        _ = k.PopSelection();
    }
}
