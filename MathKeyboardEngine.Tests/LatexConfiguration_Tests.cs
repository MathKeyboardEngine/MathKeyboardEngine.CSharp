using Xunit;

namespace MathKeyboardEngine.Tests;
public class LatexConfiguration_Tests
{
    [Fact]
    public void Allow_customizing_the_shape_of_the_cursor_and_empty_Placeholders()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new AscendingBranchingNode("", "^{", "}"));
        // Act
        var myLatexConfiguration = new LatexConfiguration
        {
            ActivePlaceholderShape = "myCursor",
            PassivePlaceholderShape = "myEmptyPlace"
        };
        // Assert
        Assert.Equal("myCursor^{myEmptyPlace}", k.GetEditModeLatex(myLatexConfiguration));
    }

    [Fact]
    public void Allows_customizing_the_color_of_the_cursor_and_Placeholders()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new AscendingBranchingNode("", "^{", "}"));
        var myLatexConfiguration = new LatexConfiguration
        {
            ActivePlaceholderShape = @"\blacksquare",
            PassivePlaceholderShape = @"\blacksquare",
            // Act
            ActivePlaceholderColor = "orange",
            PassivePlaceholderColor = "gray"
        };
        // Assert
        Assert.Equal(@"\color{orange}{\blacksquare}^{\color{gray}{\blacksquare}}", k.GetEditModeLatex(myLatexConfiguration));
    }
}
