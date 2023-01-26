using MathKeyboardEngine._Helpers;
namespace MathKeyboardEngine;
public static class _EnterSelectionMode
{
    public static void EnterSelectionMode(this KeyboardMemory k)
    {
        k.SetSelectionDiff(0);
    }
}
