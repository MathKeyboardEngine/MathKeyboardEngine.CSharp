using Xunit;

namespace MathKeyboardEngine.Tests;
public class StandardBranchingNode_Tests
{
    [Fact]
    public void Sqrt_3_right_left_left_left_right()
    {
        var k = new KeyboardMemory();
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        Expect.Latex(@"\sqrt{▦}", k);
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        Expect.Latex(@"\sqrt{3}▦", k);
        k.MoveLeft();
        Expect.Latex(@"\sqrt{3▦}", k);
        k.MoveLeft();
        Expect.Latex(@"\sqrt{▦3}", k);
        k.MoveLeft();
        Expect.Latex(@"▦\sqrt{3}", k);
        k.MoveRight();
        Expect.Latex(@"\sqrt{▦3}", k);
    }

    [Fact]
    public void Sqrt_right_left_left_left_right()
    {
        var k = new KeyboardMemory();
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        Expect.Latex(@"\sqrt{▦}", k);
        k.MoveRight();
        Expect.Latex(@"\sqrt{⬚}▦", k);
        k.MoveLeft();
        Expect.Latex(@"\sqrt{▦}", k);
        k.MoveLeft();
        Expect.Latex(@"▦\sqrt{⬚}", k);
        k.MoveRight();
        Expect.Latex(@"\sqrt{▦}", k);
    }

    [Fact]
    public void Sqrt_del()
    {
        var k = new KeyboardMemory();
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        Expect.Latex(@"\sqrt{▦}", k);
        k.DeleteCurrent();
        Expect.Latex("▦", k);
    }
}
