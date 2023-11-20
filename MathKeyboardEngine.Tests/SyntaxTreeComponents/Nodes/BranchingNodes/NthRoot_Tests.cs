using Xunit;

namespace MathKeyboardEngine.Tests;

public class NthRoot_Tests
{
    [Fact]
    public void Basic_test()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\sqrt[", "]{", "}"));
        Expect.EditModeLatex(@"\sqrt[▦]{⬚}", k);
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        Expect.EditModeLatex(@"\sqrt[3]{▦}", k);
        k.Insert(new DigitNode("2"));
        k.Insert(new DigitNode("7"));
        Expect.EditModeLatex(@"\sqrt[3]{27▦}", k);
    }

    [Fact]
    public void MoveUp_MoveDown__including_impossible_updown_requests()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\sqrt[", "]{", "}"));
        Expect.EditModeLatex(@"\sqrt[▦]{⬚}", k);
        // Act & Assert MoveDown
        k.MoveDown();
        Expect.EditModeLatex(@"\sqrt[⬚]{▦}", k);
        k.MoveDown();
        Expect.EditModeLatex(@"\sqrt[⬚]{▦}", k);
        // Act && Assert MoveUp
        k.MoveUp();
        Expect.EditModeLatex(@"\sqrt[▦]{⬚}", k);
        k.MoveUp();
        Expect.EditModeLatex(@"\sqrt[▦]{⬚}", k);
    }
}
