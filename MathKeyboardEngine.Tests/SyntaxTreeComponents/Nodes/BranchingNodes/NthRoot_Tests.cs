using Xunit;

namespace MathKeyboardEngine.Tests;
public class NthRoot_Tests
{
    [Fact]
    public void Basic_test()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\sqrt[", "]{", "}"));
        Expect.Latex(@"\sqrt[▦]{⬚}", k);
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        Expect.Latex(@"\sqrt[3]{▦}", k);
        k.Insert(new DigitNode("2"));
        k.Insert(new DigitNode("7"));
        Expect.Latex(@"\sqrt[3]{27▦}", k);
    }

    [Fact]
    public void MoveUp_MoveDown__including_impossible_updown_requests()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\sqrt[", "]{", "}"));
        Expect.Latex(@"\sqrt[▦]{⬚}", k);
        // Act & Assert MoveDown
        k.MoveDown();
        Expect.Latex(@"\sqrt[⬚]{▦}", k);
        k.MoveDown();
        Expect.Latex(@"\sqrt[⬚]{▦}", k);
        // Act && Assert MoveUp
        k.MoveUp();
        Expect.Latex(@"\sqrt[▦]{⬚}", k);
        k.MoveUp();
        Expect.Latex(@"\sqrt[▦]{⬚}", k);
    }
}
