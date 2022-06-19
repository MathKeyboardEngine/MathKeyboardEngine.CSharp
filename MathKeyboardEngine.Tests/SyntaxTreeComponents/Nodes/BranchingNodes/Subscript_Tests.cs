using Xunit;

namespace MathKeyboardEngine.Tests;
public class Subscript_Tests
{
    [Fact]
    public void Subscript_a_MoveRight_4()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode("", "_{", "}"));
        k.Insert(new StandardLeafNode("a"));
        k.MoveRight();
        k.Insert(new DigitNode("4"));
        Expect.Latex("a_{4▦}", k);
    }

    [Fact]
    public void Subscript_a_MoveDown_4()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode("", "_{", "}"));
        k.Insert(new StandardLeafNode("a"));
        k.MoveDown();
        k.Insert(new DigitNode("4"));
        Expect.Latex("a_{4▦}", k);
    }

    [Fact]
    public void InsertWithEncapsulateCurrent()
    {
        var k = new KeyboardMemory();
        k.Insert(new StandardLeafNode("a"));
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode("", "_{", "}"));
        Expect.Latex("a_{▦}", k);
    }

    [Fact]
    public void Subscript_a_MoveDown_4_MoveUp()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode("", "_{", "}"));
        k.Insert(new StandardLeafNode("a"));
        k.MoveDown();
        k.Insert(new DigitNode("4"));
        Expect.Latex("a_{4▦}", k);
        k.MoveUp();
        Expect.Latex("a▦_{4}", k);
    }

    [Fact]
    public void Can_be_left_empty__moving_out_and_back_in()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode("", "_{", "}"));
        Expect.Latex("▦_{⬚}", k);
        // Act & Assert
        k.MoveLeft();
        Expect.Latex("▦⬚_{⬚}", k);
        k.MoveRight();
        Expect.Latex("▦_{⬚}", k);
    }

    [Fact]
    public void Impossible_updown_requests_in_empty_subscriptNode_should_not_throw()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode("", "_{", "}"));
        Expect.Latex("▦_{⬚}", k);
        // Act & Assert 1
        k.MoveUp();
        Expect.Latex("▦_{⬚}", k);
        // Arrange 2
        k.MoveDown();
        Expect.Latex("⬚_{▦}", k);
        // Act & Assert 2
        k.MoveDown();
        Expect.Latex("⬚_{▦}", k);
    }

    [Fact]
    public void Impossible_updown_requests_in_filled_subscriptNode_should_not_throw()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode("", "_{", "}"));
        k.Insert(new StandardLeafNode("a"));
        Expect.Latex("a▦_{⬚}", k);
        // Act & Assert 1
        k.MoveUp();
        Expect.Latex("a▦_{⬚}", k);
        // Arrange 2
        k.MoveDown();
        k.Insert(new DigitNode("4"));
        Expect.Latex("a_{4▦}", k);
        // Act & Assert 2
        k.MoveDown();
        Expect.Latex("a_{4▦}", k);
    }
}
