using Xunit;

namespace MathKeyboardEngine.Tests;
public class InsertWithEncapsulateCurrent_Tests
{
    [Fact]
    public void Does_a_regular_insert_if_Current_is_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        Assert.True(k.Current is Placeholder);
        // Act
        k.InsertWithEncapsulateCurrent(new AscendingBranchingNode("", "^{", "}"));
        // Assert
        Expect.Latex("▦^{⬚}", k);
    }

    [Fact]
    public void Can_encapsulate_complex_stuff_like_matrixes()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        for (var i = 1; i <= 4; i++)
        {
            k.Insert(new DigitNode(i.ToString()));
            k.MoveRight();
        }
        // Act
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        // Assert
        Expect.Latex(@"\frac{\begin{pmatrix}1 & 2 \\ 3 & 4\end{pmatrix}}{▦}", k);
    }

    [Fact]
    public void Can_also_be_used_in__for_example__a_matrix()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        k.Insert(new DigitNode("2"));
        // Act
        k.InsertWithEncapsulateCurrent(new AscendingBranchingNode("", "^{", "}"));
        // Assert
        Expect.Latex(@"\begin{pmatrix}2^{▦} & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
    }

    [Fact]
    public void Can_encapsulate_multiple_digits()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        // Act
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        // Assert
        Expect.Latex(@"\frac{12}{▦}", k);
    }

    [Fact]
    public void Can_encapsulate_a_decimal_number()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.Insert(new DecimalSeparatorNode());
        k.Insert(new DigitNode("3"));
        // Act
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        // Assert
        Expect.Latex(@"\frac{12.3}{▦}", k);
    }

    [Fact]
    public void Does_not_encapsulate_more_than_it_should()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new StandardLeafNode("+"));
        k.Insert(new DigitNode("2"));
        k.Insert(new DigitNode("3"));
        // Act
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        // Assert
        Expect.Latex(@"1+\frac{23}{▦}", k);
    }

    [Fact]
    public void Can_encapsulate_round_brackets()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new StandardLeafNode("+"));
        k.Insert(new RoundBracketsNode("(", ")"));
        k.Insert(new DigitNode("2"));
        k.Insert(new StandardLeafNode("+"));
        k.Insert(new DigitNode("3"));
        Expect.Latex(@"1+(2+3▦)", k);

        k.MoveRight();
        Expect.Latex(@"1+(2+3)▦", k);
        var powerNode = new AscendingBranchingNode("", "^{", "}");
        // Act
        k.InsertWithEncapsulateCurrent(powerNode);
        // Assert
        Expect.Latex(@"1+(2+3)^{▦}", k);
        Assert.Equal("(2+3)", powerNode.Placeholders[0].GetLatex(k, null!));
    }

    // Continue port here
}
