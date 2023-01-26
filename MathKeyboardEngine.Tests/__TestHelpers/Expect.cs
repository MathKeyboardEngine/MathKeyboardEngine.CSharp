using Xunit;

namespace MathKeyboardEngine.Tests;
public static class Expect
{
    private static readonly LatexConfiguration _testConfig = new LatexConfiguration
    {
        ActivePlaceholderShape = "▦",
        PassivePlaceholderShape = "⬚",
        SelectionHightlightStart = @"\colorbox{blue}{",
        SelectionHightlightEnd = "}",
    };

    public static void Latex(string latex, KeyboardMemory k)
    {
        Assert.Equal(latex, k.GetEditModeLatex(_testConfig));
    }

    public static void ViewModeLatex(string latex, KeyboardMemory k)
    {
        Assert.Equal(latex, k.GetViewModeLatex(_testConfig));
    }
}
