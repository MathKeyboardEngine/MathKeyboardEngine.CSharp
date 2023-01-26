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

    [Fact]
    public void With_DeleteOuterRoundBracketsIfAny__deletes_outer_round_brackets_during_encapsulation()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new StandardLeafNode("+"));
        k.Insert(new RoundBracketsNode("(", ")"));
        k.Insert(new RoundBracketsNode("(", ")"));
        k.Insert(new StandardLeafNode("x"));
        k.Insert(new StandardLeafNode("+"));
        k.Insert(new DigitNode("2"));
        k.MoveRight();
        k.Insert(new RoundBracketsNode("(", ")"));
        k.Insert(new StandardLeafNode("x"));
        k.Insert(new StandardLeafNode("-"));
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        k.MoveRight();
        Expect.Latex(@"1+((x+2)(x-3))▦", k);
        // Act
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"), InsertWithEncapsulateCurrentOptions.DeleteOuterRoundBracketsIfAny);
        // Assert
        Expect.Latex(@"1+\frac{(x+2)(x-3)}{▦}", k);
    }

    [Fact]
    public void With_DeleteOuterRoundBracketsIfAny__does_not_delete_square_brackets_during_encapsulation()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new StandardLeafNode("+"));
        k.Insert(new StandardBranchingNode("|", "|"));
        k.Insert(new StandardLeafNode("x"));
        k.Insert(new StandardLeafNode("+"));
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        Expect.Latex(@"1+|x+3|▦", k);
        // Act
        var fraction = new DescendingBranchingNode(@"\frac{", "}{", "}");
        k.InsertWithEncapsulateCurrent(fraction, InsertWithEncapsulateCurrentOptions.DeleteOuterRoundBracketsIfAny);
        // Assert
        Expect.Latex(@"1+\frac{|x+3|}{▦}", k);
    }

    [Fact]
    public void With_DeleteOuterRoundBracketsIfAny__encapsulation_by_single_Placeholder_BranchingNode_sets_the_cursor_at_the_right_of_the_new_BranchingNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new RoundBracketsNode("(", ")"));
        k.Insert(new StandardLeafNode("A"));
        k.Insert(new StandardLeafNode("B"));
        k.MoveRight();
        Expect.Latex("(AB)▦", k);
        // Act
        k.InsertWithEncapsulateCurrent(new StandardBranchingNode(@"\overrightarrow{", "}"), InsertWithEncapsulateCurrentOptions.DeleteOuterRoundBracketsIfAny);
        // Assert
        Expect.Latex(@"\overrightarrow{AB}▦", k);

    }
}
