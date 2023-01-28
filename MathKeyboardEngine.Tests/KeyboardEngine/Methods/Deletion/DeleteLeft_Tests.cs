using Xunit;

namespace MathKeyboardEngine.Tests;

public class DeleteLeft_Tests
{
    [Fact]
    public void DeleteLeft_can_also_be_used_to_delete_empty_Placeholders_in_some_cases_UX__case_x()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("2"));
        k.Insert(new StandardLeafNode("x"));
        k.Insert(new StandardLeafNode("+")); // oops, typo!
        k.InsertWithEncapsulateCurrent(new AscendingBranchingNode("", "^{", "}"));
        k.Insert(new DigitNode("3"));
        k.MoveDown();
        k.DeleteLeft(); // trying to fix typo
        Expect.Latex("2x▦^{3}", k);
        k.MoveUp();
        Expect.Latex("2x⬚^{3▦}", k); // Huh? Let's delete that empty placeholder!
        k.MoveDown();
        Expect.Latex("2x▦^{3}", k);
        // Act
        k.DeleteLeft();
        k.MoveUp();
        // Assert
        Expect.Latex("2x^{3▦}", k);
    }

    [Fact]
    public void DeleteLeft_can_also_be_used_to_delete_empty_Placeholders_in_some_cases_UX__case_1plus2point5()
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
        k.DeleteLeft(); // trying to fix typo
        Expect.Latex("1+2.5▦^{3}", k);
        k.MoveUp();
        Expect.Latex("1+2.5⬚^{3▦}", k); // Huh? Let's delete that empty placeholder!
        k.MoveDown();
        Expect.Latex("1+2.5▦^{3}", k);
        // Act
        k.DeleteLeft();
        Expect.Latex("1+2.5▦^{3}", k);
        k.MoveUp();
        // Assert
        Expect.Latex("1+2.5^{3▦}", k);
    }

    [Fact]
    public void DeleteLeft_can_also_be_used_to_delete_empty_Placeholders_in_some_cases_UX__case_2point5()
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
        k.DeleteLeft(); // trying to fix typo
        Expect.Latex("2.5▦^{3}", k);
        k.MoveUp();
        Expect.Latex("2.5⬚^{3▦}", k); // Huh? Let's delete that empty placeholder!
        k.MoveDown();
        Expect.Latex("2.5▦^{3}", k);
        // Act
        k.DeleteLeft();
        k.MoveUp();
        // Assert
        Expect.Latex("2.5^{3▦}", k);
    }

    [Fact]
    public void DeleteLeft_does_nothing_sometimes()
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
        k.DeleteLeft();
        // Assert
        Expect.Latex(@"\begin{pmatrix}⬚ & ▦ \\ 3 & ⬚\end{pmatrix}", k);
    }

    [Fact]
    public void DeleteLeft_deletes_the_last_TreeNode_from_the_previous_Placeholders()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}12 & ▦ \\ ⬚ & ⬚\end{pmatrix}", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex(@"\begin{pmatrix}1▦ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
    }

    [Fact]
    public void DeleteLeft_can_revert_InsertWithEncapsulateCurrent_sometimes__execution_path_with_multiple_digits_treated_as_a_single_thing()
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
        k.DeleteLeft();
        Expect.Latex("2^{3▦}", k);
        Assert.Equal(d3.ParentPlaceholder, powerNode.Placeholders[1]);
        k.DeleteLeft();
        Expect.Latex("2^{▦}", k);
    }


    [Fact]
    public void DeleteLeft_can_delete_from_the_first_Placeholder_of_a_BranchingNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        Expect.Latex(@"\frac{12▦}{⬚}", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex(@"\frac{1▦}{⬚}", k);
    }

    [Fact]
    public void DeleteLeft_can_revert__raise_selected_to_the_power_of_an_empty_Placeholder()
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
        k.DeleteLeft();
        // Assert
        Expect.Latex("12▦", k);
    }

    [Fact]
    public void DeleteLeft_from_the_right_of_a_single_Placeholder_BranchingNode__Placeholder_contains_a_TreeNodes()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new RoundBracketsNode("(", ")"));
        k.Insert(new DigitNode("1"));
        k.Insert(new StandardLeafNode("+"));
        k.Insert(new StandardLeafNode("x"));
        k.MoveRight();
        Expect.Latex("(1+x)▦", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex("1+x▦", k);

    }

    [Fact]
    public void DeleteLeft_from_the_right_of_a_BranchingNode__last_Placeholder_contains_a_LeafNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.Insert(new StandardLeafNode("x"));
        k.MoveRight();
        Expect.Latex(@"\frac{1}{x}▦", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex(@"\frac{1}{▦}", k);
    }

    [Fact]
    public void DeleteLeft_from_the_right_of_a_BranchingNode__last_Placeholder_contains_nested_BranchingNodes()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.Insert(new DigitNode("1"));
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.Insert(new DigitNode("1"));
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.Insert(new StandardLeafNode("x"));
        k.MoveRight();
        k.MoveRight();
        k.MoveRight();
        Expect.Latex(@"\frac{1}{\frac{1}{\frac{1}{x}}}▦", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex(@"\frac{1}{\frac{1}{\frac{1}{▦}}}", k);
    }

    [Fact]
    public void DeleteLeft_from_the_right_of_a_BranchingNode__last_Placeholder_is_empty_and_first_Placeholder_contains_1_LeafNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.MoveRight();
        k.MoveRight();
        Expect.Latex(@"\frac{1}{⬚}▦", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex("1▦", k);
    }

    [Fact]
    public void DeleteLeft_deletes_a_subscript_from_its_empty_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode("", "_{", "}"));
        Expect.Latex("12_{▦}", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex("12▦", k);
    }

    [Fact]
    public void DeleteLeft_deletes_a_subscript_from_the_right()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode("", "_{", "}"));
        k.MoveRight();
        k.MoveRight();
        Expect.Latex("12_{⬚}▦", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex("12▦", k);
    }

    [Fact]
    public void DeleteLeft_deletes_a_subscript_from_the_right__case_with_a_BranchingNode_on_the_right()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode("", "_{", "}"));
        Expect.Latex(@"12_{▦}", k);
        k.MoveRight();
        Expect.Latex(@"12_{⬚}▦", k);
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        Expect.Latex(@"12_{⬚}\sqrt{▦}", k);
        k.MoveLeft();
        Expect.Latex(@"12_{⬚}▦\sqrt{⬚}", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex(@"12▦\sqrt{⬚}", k);
    }

    [Fact]
    public void DeleteLeft_deletes_a_single_column_matrix_or_any_BranchingNode_from_the_right_if_the_only_non_empty_Placeholder_is_at_index_0()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 1, 3));
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.MoveDown();
        k.MoveRight();
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}12 \\ ⬚ \\ ⬚\end{pmatrix}▦", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex("12▦", k);
    }

    [Fact]
    public void DeleteLeft_deletes_a_fraction_from_its_second_Placeholder__case_with_a_BranchingNode_on_the_right()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.Insert(new StandardLeafNode("a"));
        k.Insert(new StandardLeafNode("b"));
        k.MoveDown();
        k.MoveRight();
        Expect.Latex(@"\frac{ab}{⬚}▦", k);
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        Expect.Latex(@"\frac{ab}{⬚}\sqrt{▦}", k);
        k.MoveLeft();
        Expect.Latex(@"\frac{ab}{⬚}▦\sqrt{⬚}", k);
        k.MoveLeft();
        Expect.Latex(@"\frac{ab}{▦}\sqrt{⬚}", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex(@"ab▦\sqrt{⬚}", k);
    }

    [Fact]
    public void DeleteLeft_deletes_the_last_TreeNode_of_the_last_Placeholder_with_content()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.MoveDown();
        k.Insert(new DigitNode("3"));
        k.Insert(new DigitNode("4"));
        k.MoveRight();
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}12 & ⬚ \\ 34 & ⬚\end{pmatrix}▦", k);
        // Act & Assert
        k.DeleteLeft();
        Expect.Latex(@"\begin{pmatrix}12 & ⬚ \\ 3▦ & ⬚\end{pmatrix}", k);
        k.DeleteLeft();
        Expect.Latex(@"\begin{pmatrix}12 & ⬚ \\ ▦ & ⬚\end{pmatrix}", k);
        k.DeleteLeft();
        Expect.Latex(@"\begin{pmatrix}1▦ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
    }

    [Fact]
    public void DeleteLeft_does_nothing_from_the_first_Placeholder_if_multiple_sibling_Placeholders_are_filled()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        k.MoveRight();
        k.Insert(new DigitNode("2"));
        k.MoveDown();
        k.Insert(new DigitNode("4"));
        k.MoveLeft();
        k.MoveLeft();
        k.MoveUp();
        Expect.Latex(@"\begin{pmatrix}▦ & 2 \\ ⬚ & 4\end{pmatrix}", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex(@"\begin{pmatrix}▦ & 2 \\ ⬚ & 4\end{pmatrix}", k);
    }

    [Fact]
    public void DeleteLeft_deletes_a_BranchingNode_from_one_of_its_Placeholders__sets_Current_at_the_right_of_the_previous_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("2"));
        k.Insert(new StandardLeafNode(@"\times"));
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        Expect.Latex(@"2\times\begin{pmatrix}▦ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
        // Act
        k.DeleteLeft();
        // Assert
        Expect.Latex(@"2\times▦", k);
    }
}
