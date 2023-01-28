using Xunit;

namespace MathKeyboardEngine.Tests;

public class StandardLeafNode_Tests
{
    [Fact]
    public void The_StandardLeafNode_allows_customizing_the_multiplication_operator_sign_even_if_it_is_already_in_the_KeyboardMemorys_syntax_tree()
    {
        // Arrange
        var myMultiplicationSignSetting = @"\times";
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("2"));
        k.Insert(new StandardLeafNode(() => myMultiplicationSignSetting));
        k.Insert(new StandardLeafNode("a"));
        Expect.Latex(@"2\times a▦", k);
        // Act
        myMultiplicationSignSetting = @"\cdot";
        // Assert
        Expect.Latex(@"2\cdot a▦", k);
    }
}
