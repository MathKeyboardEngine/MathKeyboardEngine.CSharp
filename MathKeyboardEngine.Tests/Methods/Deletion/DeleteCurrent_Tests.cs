using Xunit;

namespace MathKeyboardEngine.Tests;
public class DeleteCurrent_Tests
{
    [Fact]
    public void DeleteCurrent_can_also_be_used_to_delete_empty_Placeholders_in_some_cases_UX__case_x()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("2"));
        k.Insert(new StandardLeafNode("x"));
        k.Insert(new StandardLeafNode("+")); // oops, typo!
        k.InsertWithEncapsulateCurrent(new AscendingBranchingNode("", "^{", "}"));
        k.Insert(new DigitNode("3"));
        k.MoveDown();
        k.DeleteCurrent(); // trying to fix typo
        Expect.Latex("2x▦^{3}", k);
        k.MoveUp();
        Expect.Latex("2x⬚^{3▦}", k); // Huh? Let's delete that empty placeholder!
        k.MoveDown();
        Expect.Latex("2x▦^{3}", k);
        // Act
        k.DeleteCurrent();
        k.MoveUp();
        // Assert
        Expect.Latex("2x^{3▦}", k);
    }

    [Fact]
    public void DeleteCurrent_can_also_be_used_to_delete_empty_Placeholders_in_some_cases_UX__case_1plus2point5()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new StandardLeafNode("+"));
        k.Insert(new DigitNode("2"));
        k.Insert(new DecimalSeparatorNode());
        k.Insert(new DigitNode("5"));
        k.Insert(new StandardLeafNode("+")); // oops, typo!
        k.InsertWithEncapsulateCurrent(new AscendingBranchingNode("", "^{", "}"));
        Expect.Latex("1+2.5+^{▦}", k);
        k.Insert(new DigitNode("3"));
        Expect.Latex("1+2.5+^{3▦}", k);
        k.MoveDown();
        k.DeleteCurrent(); // trying to fix typo
        Expect.Latex("1+2.5▦^{3}", k);
        k.MoveUp();
        Expect.Latex("1+2.5⬚^{3▦}", k); // Huh? Let's delete that empty placeholder!
        k.MoveDown();
        Expect.Latex("1+2.5▦^{3}", k);
        // Act
        k.DeleteCurrent();
        Expect.Latex("1+2.5▦^{3}", k);
        k.MoveUp();
        // Assert
        Expect.Latex("1+2.5^{3▦}", k);
    }

    [Fact]
    public void DeleteCurrent_can_also_be_used_to_delete_empty_Placeholders_in_some_cases_UX__case_2point5()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("2"));
        k.Insert(new DecimalSeparatorNode());
        k.Insert(new DigitNode("5"));
        k.Insert(new StandardLeafNode("+")); // oops, typo!
        k.InsertWithEncapsulateCurrent(new AscendingBranchingNode("", "^{", "}"));
        k.Insert(new DigitNode("3"));
        k.MoveDown();
        k.DeleteCurrent(); // trying to fix typo
        Expect.Latex("2.5▦^{3}", k);
        k.MoveUp();
        Expect.Latex("2.5⬚^{3▦}", k); // Huh? Let's delete that empty placeholder!
        k.MoveDown();
        Expect.Latex("2.5▦^{3}", k);
        // Act
        k.DeleteCurrent();
        k.MoveUp();
        // Assert
        Expect.Latex("2.5^{3▦}", k);
    }

    [Fact]
    public void DeleteCurrent_does_nothing_sometimes()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        k.MoveDown();
        k.Insert(new DigitNode("3"));
        k.MoveUp();
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}⬚ & ▦ \\ 3 & ⬚\end{pmatrix}", k);
        // Act
        k.DeleteCurrent();
        // Assert
        Expect.Latex(@"\begin{pmatrix}⬚ & ▦ \\ 3 & ⬚\end{pmatrix}", k);
    }

    [Fact]
    public void DeleteCurrent_deletes_the_last_TreeNode_from_the_previous_Placeholders()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}12 & ▦ \\ ⬚ & ⬚\end{pmatrix}", k);
        // Act
        k.DeleteCurrent();
        // Assert
        Expect.Latex(@"\begin{pmatrix}1▦ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
    }

    [Fact]
    public void DeleteCurrent_can_revert_InsertWithEncapsulateCurrent_sometimes__execution_path_with_multiple_digits_treated_as_a_single_thing()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("2"));
        var powerNode = new AscendingBranchingNode("", "^{", "}");
        k.InsertWithEncapsulateCurrent(powerNode);
        var d3 = new DigitNode("3");
        k.Insert(d3);
        k.InsertWithEncapsulateCurrent(new AscendingBranchingNode("", "^{", "}"));
        Expect.Latex("2^{3^{▦}}", k);
        // Act & Assert
        k.DeleteCurrent();
        Expect.Latex("2^{3▦}", k);
        Assert.Equal(d3.ParentPlaceholder, powerNode.Placeholders[1]);
        k.DeleteCurrent();
        Expect.Latex("2^{▦}", k);
    }


    [Fact]
    public void DeleteCurrent_can_delete_from_the_first_Placeholder_of_a_BranchingNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        Expect.Latex(@"\frac{12▦}{⬚}", k);
        // Act
        k.DeleteCurrent();
        // Assert
        Expect.Latex(@"\frac{1▦}{⬚}", k);
    }

    [Fact]
    public void DeleteCurrent_can_revert__raise_selected_to_the_power_of_an_empty_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        Expect.Latex("12▦", k);
        k.SelectLeft();
        k.SelectLeft();
        Expect.Latex(@"\colorbox{blue}{12}", k);
        k.InsertWithEncapsulateSelectionAndPrevious(new AscendingBranchingNode("", "^{", "}"));
        Expect.Latex("⬚^{12▦}", k);
        k.MoveDown();
        Expect.Latex("▦^{12}", k);
        // Act
        k.DeleteCurrent();
        // Assert
        Expect.Latex("12▦", k);
    }

    // Continue port here
}
