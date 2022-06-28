using MathKeyboardEngine;

namespace BlazorWebAssemblyAppWithKatex;

public static class CssHelper
{
    public static string GetSelectionModeClass(KeyboardMemory k)
    {
        return k.InSelectionMode() ? "inSelectionMode" : "";
    }
}
