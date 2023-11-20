using Xunit;

namespace MathKeyboardEngine.Tests;

public class DeleteRight_Tests
{
    [Fact]
    public void DeleteRight_can_delete_an_empty_single_Placeholder_BranchingNode_from_its_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        Expect.EditModeLatex(@"\sqrt{▦}", k);
        k.MoveRight();
        k.Insert(new DigitNode("1"));
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"\sqrt{▦}1", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex("▦1", k);
        k.DeleteRight();
        Expect.EditModeLatex("▦", k);
    }

    [Fact]
    public void DeleteRight_can_delete_an_empty_multi_Placeholder_BranchingNode_from_any_Placeholder__case__first()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        Expect.EditModeLatex(@"\frac{▦}{⬚}", k);
        k.MoveRight();
        k.MoveRight();
        k.Insert(new DigitNode("1"));
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"\frac{▦}{⬚}1", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex("▦1", k);
        k.DeleteRight();
        Expect.EditModeLatex("▦", k);
    }

    [Fact]
    public void DeleteRight_can_delete_an_empty_multi_Placeholder_BranchingNode_from_any_Placeholder__case__last()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        Expect.EditModeLatex(@"\frac{▦}{⬚}", k);
        k.MoveRight();
        k.MoveRight();
        k.Insert(new DigitNode("1"));
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"\frac{⬚}{▦}1", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex("▦1", k);
        k.DeleteRight();
        Expect.EditModeLatex("▦", k);
    }

    [Fact]
    public void DeleteRight_does_nothing_if_an_empty_SyntaxTreeRoot_is_Current()
    {
        // Arrange
        var k = new KeyboardMemory();
        // Act
        k.DeleteRight();
        // Assert
        Expect.EditModeLatex("▦", k);
    }

    [Fact]
    public void DeleteRight_does_nothing_if_there_are_only_TreeNodes_on_the_left_instead_of_the_right()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        Expect.EditModeLatex("1▦", k);
        // Act
        k.DeleteRight();
        // Assert
        Expect.EditModeLatex("1▦", k);
    }

    [Fact]
    public void DeleteRight_deletes_LeafNodes_and_empty_BranchingNodes_that_are_on_the_right_of_the_cursor__Current_is_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        k.MoveRight();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        Expect.EditModeLatex(@"12\sqrt{⬚}\frac{▦}{⬚}", k);
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"▦12\sqrt{⬚}\frac{⬚}{⬚}", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex(@"▦2\sqrt{⬚}\frac{⬚}{⬚}", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"▦\sqrt{⬚}\frac{⬚}{⬚}", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"▦\frac{⬚}{⬚}", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"▦", k);
    }

    [Fact]
    public void DeleteRight_deletes_LeafNodes_and_empty_BranchingNodes_that_are_on_the_right_of_the_cursor__Current_is_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        k.MoveRight();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        Expect.EditModeLatex(@"12\sqrt{⬚}\frac{▦}{⬚}", k);
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"1▦2\sqrt{⬚}\frac{⬚}{⬚}", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex(@"1▦\sqrt{⬚}\frac{⬚}{⬚}", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"1▦\frac{⬚}{⬚}", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"1▦", k);
    }

    [Fact]
    public void DeleteRight_deletes_non_empty_single_Placeholder_BranchingNodes_in_parts__Current_is_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        k.Insert(new DigitNode("1"));
        k.Insert(new StandardLeafNode("-"));
        k.Insert(new StandardLeafNode("x"));
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"▦\sqrt{1-x}", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex(@"▦1-x", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"▦-x", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"▦x", k);
        k.DeleteRight();
        Expect.EditModeLatex("▦", k);
    }

    [Fact]
    public void DeleteRight_deletes_non_empty_single_Placeholder_BranchingNodes_in_parts__Current_is_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("7"));
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        k.Insert(new DigitNode("1"));
        k.Insert(new StandardLeafNode("-"));
        k.Insert(new StandardLeafNode("x"));
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"7▦\sqrt{1-x}", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex(@"7▦1-x", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"7▦-x", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"7▦x", k);
        k.DeleteRight();
        Expect.EditModeLatex("7▦", k);
    }

    [Fact]
    public void DeleteRight_steps_into_complex_BranchingNodes__Current_is_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("7"));
        k.Insert(new RoundBracketsNode("(", ")"));
        k.Insert(new DigitNode("1"));
        k.Insert(new StandardLeafNode("-"));
        k.Insert(new StandardLeafNode("x"));
        k.MoveRight();
        k.InsertWithEncapsulateCurrent(new AscendingBranchingNode("", "^{", "}"));
        k.Insert(new DigitNode("2"));
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"7▦(1-x)^{2}", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex("7▦1-x^{2}", k);
        k.DeleteRight();
        Expect.EditModeLatex("7▦-x^{2}", k);
        k.DeleteRight();
        Expect.EditModeLatex("7▦x^{2}", k);
        k.DeleteRight();
        Expect.EditModeLatex("7▦^{2}", k);
        k.DeleteRight();
        Expect.EditModeLatex("7▦2", k);
        k.DeleteRight();
        Expect.EditModeLatex("7▦", k);
    }

    [Fact]
    public void DeleteRight_steps_into_complex_BranchingNodes__Current_is_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new RoundBracketsNode("(", ")"));
        k.Insert(new DigitNode("1"));
        k.Insert(new StandardLeafNode("-"));
        k.Insert(new StandardLeafNode("x"));
        k.MoveRight();
        k.InsertWithEncapsulateCurrent(new AscendingBranchingNode("", "^{", "}"));
        k.Insert(new DigitNode("2"));
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"▦(1-x)^{2}", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex("▦1-x^{2}", k);
        k.DeleteRight();
        Expect.EditModeLatex("▦-x^{2}", k);
        k.DeleteRight();
        Expect.EditModeLatex("▦x^{2}", k);
        k.DeleteRight();
        Expect.EditModeLatex("▦^{2}", k);
        k.DeleteRight();
        Expect.EditModeLatex("▦2", k);
        k.DeleteRight();
        Expect.EditModeLatex("▦", k);
    }

    [Fact]
    public void DeleteRight_can_delete_a_MatrixNode_with_content_gaps()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        k.MoveRight();
        k.Insert(new DigitNode("1"));
        k.MoveDown();
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        Expect.EditModeLatex(@"\begin{pmatrix}⬚ & 1 \\ ⬚ & \sqrt{▦}\end{pmatrix}", k);
        k.MoveUp();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"▦\begin{pmatrix}⬚ & 1 \\ ⬚ & \sqrt{⬚}\end{pmatrix}", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex(@"\begin{pmatrix}⬚ & ▦ \\ ⬚ & \sqrt{⬚}\end{pmatrix}", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"\begin{pmatrix}⬚ & ⬚ \\ ⬚ & ▦\end{pmatrix}", k);
        k.DeleteRight();
        Expect.EditModeLatex("▦", k);
    }

    [Fact]
    public void DeleteRight_can_delete_a_MatrixNode_full_content()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        k.Insert(new DigitNode("1"));
        k.MoveRight();
        k.Insert(new DigitNode("2"));
        k.MoveRight();
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        k.Insert(new DigitNode("4"));
        Expect.EditModeLatex(@"\begin{pmatrix}1 & 2 \\ 3 & 4▦\end{pmatrix}", k);
        k.MoveUp();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"▦\begin{pmatrix}1 & 2 \\ 3 & 4\end{pmatrix}", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex(@"\begin{pmatrix}▦ & 2 \\ 3 & 4\end{pmatrix}", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"\begin{pmatrix}⬚ & ▦ \\ 3 & 4\end{pmatrix}", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"\begin{pmatrix}⬚ & ⬚ \\ ▦ & 4\end{pmatrix}", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"\begin{pmatrix}⬚ & ⬚ \\ ⬚ & ▦\end{pmatrix}", k);
        k.DeleteRight();
        Expect.EditModeLatex("▦", k);
    }

    [Fact]
    public void DeleteRight_does_not_delete_a_MatrixNode_from_an_empty_Placeholder_if_a_previous_Placeholder_is_not_empty()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        k.Insert(new DigitNode("1"));
        k.MoveRight();
        Expect.EditModeLatex(@"\begin{pmatrix}1 & ▦ \\ ⬚ & ⬚\end{pmatrix}", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex(@"\begin{pmatrix}1 & ▦ \\ ⬚ & ⬚\end{pmatrix}", k);
    }

    [Fact]
    public void DeleteRight_lets_the_cursor_pull_exponents_and_subscripts_towards_itself()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new AscendingBranchingNode("", "^{", "}"));
        k.MoveRight();
        k.Insert(new DigitNode("2"));
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"▦⬚^{2}", k);
        // Act & assert
        k.DeleteRight();
        Expect.EditModeLatex(@"▦2", k);
        k.DeleteRight();
        Expect.EditModeLatex(@"▦", k);
    }
}
