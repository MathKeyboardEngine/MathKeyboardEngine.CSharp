using Xunit;

namespace MathKeyboardEngine.Tests;

public class StandardBranchingNode_Tests
{
    [Fact]
    public void Sqrt_3_right_left_left_left_right()
    {
        var k = new KeyboardMemory();
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        Expect.EditModeLatex(@"\sqrt{▦}", k);
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        Expect.EditModeLatex(@"\sqrt{3}▦", k);
        k.MoveLeft();
        Expect.EditModeLatex(@"\sqrt{3▦}", k);
        k.MoveLeft();
        Expect.EditModeLatex(@"\sqrt{▦3}", k);
        k.MoveLeft();
        Expect.EditModeLatex(@"▦\sqrt{3}", k);
        k.MoveRight();
        Expect.EditModeLatex(@"\sqrt{▦3}", k);
    }

    [Fact]
    public void Sqrt_right_left_left_left_right()
    {
        var k = new KeyboardMemory();
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        Expect.EditModeLatex(@"\sqrt{▦}", k);
        k.MoveRight();
        Expect.EditModeLatex(@"\sqrt{⬚}▦", k);
        k.MoveLeft();
        Expect.EditModeLatex(@"\sqrt{▦}", k);
        k.MoveLeft();
        Expect.EditModeLatex(@"▦\sqrt{⬚}", k);
        k.MoveRight();
        Expect.EditModeLatex(@"\sqrt{▦}", k);
    }

    [Fact]
    public void Sqrt_del()
    {
        var k = new KeyboardMemory();
        k.Insert(new StandardBranchingNode(@"\sqrt{", "}"));
        Expect.EditModeLatex(@"\sqrt{▦}", k);
        k.DeleteLeft();
        Expect.EditModeLatex("▦", k);
    }
}
