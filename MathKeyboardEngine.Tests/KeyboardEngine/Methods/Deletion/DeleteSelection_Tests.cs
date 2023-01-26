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
        Expect.Latex("12▦", k);
        k.SelectLeft();
        Expect.Latex(@"1\colorbox{blue}{2}", k);
        // Act
        k.DeleteSelection();
        // Assert
        Expect.Latex("1▦", k);
    }

    [Fact]
    public void Can_delete_a_single_TreeNode__case_The_exclusive_left_border_is_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        Expect.Latex("1▦", k);
        k.SelectLeft();
        Expect.Latex(@"\colorbox{blue}{1}", k);
        // Act
        k.DeleteSelection();
        // Assert
        Expect.Latex("▦", k);
    }

    [Fact]
    public void Can_delete_multiple_TreeNodes__case_The_exclusive_left_border_is_a_TreeNode__via_SelectLeft()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.Insert(new DigitNode("3"));
        Expect.Latex("123▦", k);
        k.SelectLeft();
        k.SelectLeft();
        Expect.Latex(@"1\colorbox{blue}{23}", k);
        // Act
        k.DeleteSelection();
        // Assert
        Expect.Latex("1▦", k);
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
        Expect.Latex("1▦23", k);
        k.SelectRight();
        k.SelectRight();
        Expect.Latex(@"1\colorbox{blue}{23}", k);
        // Act
        k.DeleteSelection();
        // Assert
        Expect.Latex("1▦", k);
    }

    [Fact]
    public void Can_delete_multiple_TreeNodes__case_The_exclusive_left_border_is_a_Placeholder__via_SelectLeft()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        Expect.Latex("12▦", k);
        k.SelectLeft();
        k.SelectLeft();
        Expect.Latex(@"\colorbox{blue}{12}", k);
        // Act
        k.DeleteSelection();
        // Assert
        Expect.Latex("▦", k);
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
        Expect.Latex("▦12", k);
        k.SelectRight();
        k.SelectRight();
        Expect.Latex(@"\colorbox{blue}{12}", k);
        // Act
        k.DeleteSelection();
        // Assert
        Expect.Latex("▦", k);
    }
}
