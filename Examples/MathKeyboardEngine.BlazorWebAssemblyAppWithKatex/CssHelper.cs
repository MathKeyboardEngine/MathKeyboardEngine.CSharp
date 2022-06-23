namespace MathKeyboardEngine.BlazorWebAssemblyAppWithKatex;

public static class CssHelper
{
    public static string GetSelectionModeClass(KeyboardMemory k)
    {
        return k.InSelectionMode() ? "inSelectionMode" : "";
    }

    public const string SelectionModeColor = "#add8e6";
}
