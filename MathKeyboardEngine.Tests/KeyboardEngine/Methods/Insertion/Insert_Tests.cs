using Xunit;

namespace MathKeyboardEngine.Tests;

public class Insert_Tests
{
    [Fact]
    public void Insert_inserts_at_index_0_of_a_TreeNodeList_if_KeyboardMemory_Current_is_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        var d1 = new DigitNode("1");
        k.Insert(d1);
        Expect.EditModeLatex("1▦", k);
        k.MoveLeft();
        Assert.Equal(k.Current, d1.ParentPlaceholder);
        Expect.EditModeLatex("▦1", k);
        // Act
        k.Insert(new DigitNode("2"));
        // Assert
        Expect.EditModeLatex("2▦1", k);
    }

    [Fact]
    public void Insert_inserts_at_the_right_of_a_TreeNode_if_KeyboardMemory_Current_is_a_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        var d1 = new DigitNode("1");
        k.Insert(d1);
        Assert.Equal(k.Current, d1);
        Expect.EditModeLatex("1▦", k);
        // Act 1
        k.Insert(new DigitNode("2"));
        // Assert 1
        Expect.EditModeLatex("12▦", k);
        // Arrange 2
        k.MoveLeft();
        Assert.Equal(k.Current, d1);
        Expect.EditModeLatex("1▦2", k);
        // Act 2
        k.Insert(new DigitNode("3"));
        // Assert 2
        Expect.EditModeLatex("13▦2", k);
    }

    [Fact]
    public void Insert_sets_the_ParentPlaceholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        var node = new DigitNode("1");
        Assert.Null(node.ParentPlaceholder);
        // Act
        k.Insert(node);
        // Assert
        Assert.NotNull(node.ParentPlaceholder);
    }

    [Fact]
    public void Insert_sets_KeyboardMemory_Current()
    {
        // Arrange
        var k = new KeyboardMemory();
        var originalCurrent = k.Current;
        // Act
        k.Insert(new DigitNode("1"));
        // Assert
        Assert.NotEqual(originalCurrent, k.Current);
    }
}
