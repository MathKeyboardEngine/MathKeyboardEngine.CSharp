using Xunit;

namespace MathKeyboardEngine.Tests;
public class DecimalSeparatorNode_Tests
{
    [Fact]
    public void The_DecimalSeparatorNode_allows_customizing_the_separator_even_if_it_is_already_in_the_KeyboardMemorys_syntax_tree()
    {
        // Arrange
        var myDecimalSeparatorSetting = "{,}";
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DecimalSeparatorNode(() => myDecimalSeparatorSetting));
        k.Insert(new DigitNode("2"));
        Expect.Latex("1{,}2▦", k);
        // Act
        myDecimalSeparatorSetting = ".";
        // Assert
        Expect.Latex("1.2▦", k);
    }
}
