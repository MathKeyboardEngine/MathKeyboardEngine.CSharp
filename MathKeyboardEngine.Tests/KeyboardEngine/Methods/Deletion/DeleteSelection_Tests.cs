using Xunit;

namespace MathKeyboardEngine.Tests;

public class DeleteSelection_Tests
{
    [Fact]
    public void Can_delete_a_single_TreeNode__case_The_exclusive_left_border_is_a_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        Expect.EditModeLatex("12▦", k);
        k.SelectLeft();
        Expect.EditModeLatex(@"1\colorbox{blue}{2}", k);
        // Act
        k.DeleteSelection();
        // Assert
        Expect.EditModeLatex("1▦", k);
    }

    [Fact]
    public void Can_delete_a_single_TreeNode__case_The_exclusive_left_border_is_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        Expect.EditModeLatex("1▦", k);
        k.SelectLeft();
        Expect.EditModeLatex(@"\colorbox{blue}{1}", k);
        // Act
        k.DeleteSelection();
        // Assert
        Expect.EditModeLatex("▦", k);
    }

    [Fact]
    public void Can_delete_multiple_TreeNodes__case_The_exclusive_left_border_is_a_TreeNode__via_SelectLeft()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.Insert(new DigitNode("3"));
        Expect.EditModeLatex("123▦", k);
        k.SelectLeft();
        k.SelectLeft();
        Expect.EditModeLatex(@"1\colorbox{blue}{23}", k);
        // Act
        k.DeleteSelection();
        // Assert
        Expect.EditModeLatex("1▦", k);
    }

    [Fact]
    public void Can_delete_multiple_TreeNodes__case_The_exclusive_left_border_is_a_TreeNode__via_SelectRight()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.Insert(new DigitNode("3"));
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex("1▦23", k);
        k.SelectRight();
        k.SelectRight();
        Expect.EditModeLatex(@"1\colorbox{blue}{23}", k);
        // Act
        k.DeleteSelection();
        // Assert
        Expect.EditModeLatex("1▦", k);
    }

    [Fact]
    public void Can_delete_multiple_TreeNodes__case_The_exclusive_left_border_is_a_Placeholder__via_SelectLeft()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        Expect.EditModeLatex("12▦", k);
        k.SelectLeft();
        k.SelectLeft();
        Expect.EditModeLatex(@"\colorbox{blue}{12}", k);
        // Act
        k.DeleteSelection();
        // Assert
        Expect.EditModeLatex("▦", k);
    }

    [Fact]
    public void Can_delete_multiple_TreeNodes__case_The_exclusive_left_border_is_a_Placeholder__via_SelectRight()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.MoveLeft();
        k.MoveLeft();
        Expect.EditModeLatex("▦12", k);
        k.SelectRight();
        k.SelectRight();
        Expect.EditModeLatex(@"\colorbox{blue}{12}", k);
        // Act
        k.DeleteSelection();
        // Assert
        Expect.EditModeLatex("▦", k);
    }
}
