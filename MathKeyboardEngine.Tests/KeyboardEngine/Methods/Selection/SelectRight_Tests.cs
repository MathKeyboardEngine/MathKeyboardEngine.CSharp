using Xunit;

namespace MathKeyboardEngine.Tests;

public class SelectRight_Tests
{
    [Fact]
    public void Can_select_a_single_TreeNode_and_the_selection_is_correctly_displayed__case__the_exclusive_left_border_is_a_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.MoveLeft();
        Expect.EditModeLatex("1▦2", k);
        // Act
        k.SelectRight();
        // Assert
        Expect.EditModeLatex(@"1\colorbox{blue}{2}", k);
    }

    [Fact]
    public void Can_select_a_single_TreeNode_and_the_selection_is_correctly_displayed__case__the_exclusive_left_border_is_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.MoveLeft();
        Expect.EditModeLatex("▦1", k);
        // Act
        k.SelectRight();
        // Assert
        Expect.EditModeLatex(@"\colorbox{blue}{1}", k);
    }

    [Fact]
    public void Can_select_multiple_TreeNodes_and_the_selection_is_correctly_displayed__case__the_exclusive_left_border_is_a_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.Insert(new DigitNode("3"));
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex("1▦23", k);
        // Act
        k.SelectRight();
        k.SelectRight();
        // Assert
        Expect.EditModeLatex(@"1\colorbox{blue}{23}", k);
    }

    [Fact]
    public void Can_select_multiple_TreeNodes_and_the_selection_is_correctly_displayed__case__the_exclusive_left_border_is_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex("▦12", k);
        // Act
        k.SelectRight();
        k.SelectRight();
        // Assert
        Expect.EditModeLatex(@"\colorbox{blue}{12}", k);
    }

    [Fact]
    public void Stays_in_selection_mode_after_deselecting()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.SelectLeft();
        Expect.EditModeLatex(@"\colorbox{blue}{1}", k);
        // Act
        k.SelectRight();
        // Assert
        Expect.EditModeLatex("1▦", k);
        Assert.True(k.InSelectionMode());
    }

    [Fact]
    public void Does_nothing_if_all_on_the_right_available_TreeNodes_are_selected__case_the_exclusive_left_border_is_the_SyntaxTreeRoot()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.MoveLeft();
        Expect.EditModeLatex("▦1", k);
        k.SelectRight();
        Expect.EditModeLatex(@"\colorbox{blue}{1}", k);
        // Act
        k.SelectRight();
        // Assert
        Expect.EditModeLatex(@"\colorbox{blue}{1}", k);
    }

    [Fact]
    public void Does_nothing_if_all_on_the_right_available_TreeNodes_are_selected__case_the_exclusive_left_border_is_a_TreeNode_and_its_Parent_is_the_SyntaxTreeRoot()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.MoveLeft();
        Expect.EditModeLatex("1▦2", k);
        k.SelectRight();
        Expect.EditModeLatex(@"1\colorbox{blue}{2}", k);
        // Act
        k.SelectRight();
        // Assert
        Expect.EditModeLatex(@"1\colorbox{blue}{2}", k);
    }

    [Fact]
    public void Can_break_out_of_the_current_Placeholder__case_Set_a_Placeholder_as_the_new_Current()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        k.Insert(new DigitNode("2"));
        k.MoveRight();
        k.Insert(new StandardLeafNode("+"));
        k.Insert(new DigitNode("a"));
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"\sqrt{▦2}+a", k);
        k.SelectRight();
        Expect.EditModeLatex(@"\sqrt{\colorbox{blue}{2}}+a", k);
        // Act & Assert
        k.SelectRight();
        Expect.EditModeLatex(@"\colorbox{blue}{\sqrt{2}}+a", k);
        k.SelectRight();
        Expect.EditModeLatex(@"\colorbox{blue}{\sqrt{2}+}a", k);
        k.SelectRight();
        Expect.EditModeLatex(@"\colorbox{blue}{\sqrt{2}+a}", k);
    }

    [Fact]
    public void Can_break_out_of_the_current_Placeholder__case_Set_a_TreeNode_as_the_new_Current()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("3"));
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        k.Insert(new DigitNode("2"));
        k.MoveRight();
        k.Insert(new StandardLeafNode("+"));
        k.Insert(new StandardLeafNode("a"));
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex(@"3\sqrt{▦2}+a", k);
        k.SelectRight();
        Expect.EditModeLatex(@"3\sqrt{\colorbox{blue}{2}}+a", k);
        // Act & Assert
        k.SelectRight();
        Expect.EditModeLatex(@"3\colorbox{blue}{\sqrt{2}}+a", k);
        k.SelectRight();
        Expect.EditModeLatex(@"3\colorbox{blue}{\sqrt{2}+}a", k);
        k.SelectRight();
        Expect.EditModeLatex(@"3\colorbox{blue}{\sqrt{2}+a}", k);
    }

    [Fact]
    public void Does_nothing_in_an_empty_SyntaxTreeRoot()
    {
        var k = new KeyboardMemory();
        Expect.EditModeLatex(@"▦", k);
        k.SelectRight();
        Expect.EditModeLatex(@"▦", k);
    }
}
