using Xunit;

namespace MathKeyboardEngine.Tests;
public class GetLatex_Tests
{
    private static readonly LatexConfiguration Config = new LatexConfiguration
    {
        ActivePlaceholderShape = "▦",
        PassivePlaceholderShape = "⬚",
    };

    [Fact]
    public void Can_get_the_LaTeX_for_a_BranchingNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        // Act & Assert
        Assert.Equal(@"\frac{▦}{⬚}", k.GetEditModeLatex(Config));
        Assert.Equal(@"\frac{⬚}{⬚}", k.GetViewModeLatex(Config));
        Assert.Equal(@"\frac{⬚}{⬚}", new DescendingBranchingNode(@"\frac{", "}{", "}").GetViewModeLatex(Config));
    }

    [Fact]
    public void Can_get_the_LaTeX_for_a_LeafNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("3"));
        // Act & Assert
        Assert.Equal("3▦", k.GetEditModeLatex(Config));
        Assert.Equal("3", k.GetViewModeLatex(Config));
        Assert.Equal("3", new DigitNode("3").GetViewModeLatex(Config));
    }

    [Fact]
    public void Can_get_the_LaTeX_for_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        var fraction = new DescendingBranchingNode(@"\frac{", "}{", "}");
        k.Insert(fraction);
        k.Insert(new DigitNode("3"));
        k.MoveDown();
        // Act & Assert
        Assert.Equal(@"\frac{3}{▦}", k.GetEditModeLatex(Config));
        Assert.Equal(@"\frac{3}{⬚}", k.GetViewModeLatex(Config));
        Assert.Equal("3", fraction.Placeholders[0].GetViewModeLatex(Config));
    }
}
