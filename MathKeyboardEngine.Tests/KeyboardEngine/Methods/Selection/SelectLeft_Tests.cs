using Xunit;

namespace MathKeyboardEngine.Tests;

public class SelectLeft_Tests
{
    [Fact]
    public void Can_select_a_single_TreeNode_and_the_selection_is_correctly_displayed__case_exclusive_left_border_is_a_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        Expect.Latex("12▦", k);
        // Act
        k.SelectLeft();
        // Assert
        Expect.Latex(@"1\colorbox{blue}{2}", k);
    }

    [Fact]
    public void Can_select_a_single_TreeNode_and_the_selection_is_correctly_displayed__case_exclusive_left_border_is_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        Expect.Latex("1▦", k);
        // Act
        k.SelectLeft();
        // Assert
        Expect.Latex(@"\colorbox{blue}{1}", k);
    }

    [Fact]
    public void Can_select_mulitple_TreeNodes_and_the_selection_is_correctly_displayed__case_exclusive_left_border_is_a_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.Insert(new DigitNode("3"));
        Expect.Latex("123▦", k);
        // Act
        k.SelectLeft();
        k.SelectLeft();
        // Assert
        Expect.Latex(@"1\colorbox{blue}{23}", k);
    }

    [Fact]
    public void Can_select_mulitple_TreeNodes_and_the_selection_is_correctly_displayed__case_exclusive_left_border_is_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        Expect.Latex("12▦", k);
        // Act
        k.SelectLeft();
        k.SelectLeft();
        // Assert
        Expect.Latex(@"\colorbox{blue}{12}", k);
    }

    [Fact]
    public void Does_nothing_if_Current_is_the_SyntaxTreeRoot_and_no_SelectRight_has_been_done()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.MoveLeft();
        Expect.Latex("▦1", k);
        k.EnterSelectionMode();
        // Act
        k.SelectLeft();
        // Assert
        Expect.Latex("▦1", k);
    }

    [Fact]
    public void Does_nothing_if_all_on_the_left_available_TreeNodes_are_selected()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.SelectLeft();
        Expect.Latex(@"\colorbox{blue}{1}", k);
        // Act
        k.SelectLeft();
        // Assert
        Expect.Latex(@"\colorbox{blue}{1}", k);
    }

    [Fact]
    public void Stays_in_selection_mode_after_deselecting_until_nothing_is_selected()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.MoveLeft();
        k.SelectRight();
        Expect.Latex(@"\colorbox{blue}{1}", k);
        // Act
        k.SelectLeft();
        // Assert
        Expect.Latex("▦1", k);
        Assert.True(k.InSelectionMode());
    }

    [Fact]
    public void Can_break_out_of_the_current_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("2"));
        k.InsertWithEncapsulateCurrent(new AscendingBranchingNode("", "^{", "}"));
        k.Insert(new StandardLeafNode("x"));
        Expect.Latex("2^{x▦}", k);
        k.SelectLeft();
        Expect.Latex(@"2^{\colorbox{blue}{x}}", k);
        // Act
        k.SelectLeft();
        // Assert
        Expect.Latex(@"\colorbox{blue}{2^{x}}", k);
    }
}
