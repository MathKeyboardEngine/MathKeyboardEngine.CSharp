using Xunit;

namespace MathKeyboardEngine.Tests;

public class MoveDown_Tests
{
    [Fact]
    public void MoveDown_can_move_the_cursor_down_via_an_ancestor_if_the_current_BranchginNode_does_not_support_updown_navigation()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("2"));
        k.InsertWithEncapsulateCurrent(new AscendingBranchingNode("", "^{", "}"));
        k.Insert(new RoundBracketsNode("(", ")"));
        Expect.EditModeLatex("2^{(▦)}", k);
        // Act
        k.MoveDown();
        // Assert
        Expect.EditModeLatex("2▦^{(⬚)}", k);
    }
}
