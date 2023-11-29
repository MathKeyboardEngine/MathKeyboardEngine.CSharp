using Xunit;

namespace MathKeyboardEngine.Tests;

public static class Expect
{
    private static readonly LatexConfiguration _testConfig = new()
    {
        ActivePlaceholderShape = "▦",
        PassivePlaceholderShape = "⬚",
        SelectionHightlightStart = @"\colorbox{blue}{",
        SelectionHightlightEnd = "}",
    };

    public static void EditModeLatex(string latex, KeyboardMemory k)
    {
        Assert.Equal(latex, k.GetEditModeLatex(_testConfig));
    }

    public static void ViewModeLatex(string latex, TreeNode treeNode)
    {
        Assert.Equal(latex, treeNode.GetLatex(new KeyboardMemory(), _testConfig));
    }

    public static void ViewModeLatex(string latex, Placeholder placeholder)
    {
        Assert.Equal(latex, placeholder.GetLatex(new KeyboardMemory(), _testConfig));
    }

    public static void ViewModeLatex(string latex, KeyboardMemory k)
    {
        Assert.Equal(latex, k.GetViewModeLatex(_testConfig));
    }
}
