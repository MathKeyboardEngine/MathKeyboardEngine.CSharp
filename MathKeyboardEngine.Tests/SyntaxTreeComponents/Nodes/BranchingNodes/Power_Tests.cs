using Xunit;

namespace MathKeyboardEngine.Tests;

public class Power_Tests
{
    [Fact]
    public void Power_3_MoveRight_4()
    {
        var k = new KeyboardMemory();
        k.Insert(new AscendingBranchingNode("", "^{", "}"));
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        k.Insert(new DigitNode("4"));
        Expect.Latex("3^{4▦}", k);
    }

    [Fact]
    public void Power_3_MoveUp_4()
    {
        var k = new KeyboardMemory();
        k.Insert(new AscendingBranchingNode("", "^{", "}"));
        k.Insert(new DigitNode("3"));
        k.MoveUp();
        k.Insert(new DigitNode("4"));
        Expect.Latex("3^{4▦}", k);
    }

    [Fact]
    public void Three_encapsulated_by_Power()
    {
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("3"));
        k.InsertWithEncapsulateCurrent(new AscendingBranchingNode("", "^{", "}"));
        Expect.Latex("3^{▦}", k);
    }

    [Fact]
    public void Power_3_up_down()
    {
        var k = new KeyboardMemory();
        k.Insert(new AscendingBranchingNode("", "^{", "}"));
        k.Insert(new DigitNode("3"));
        k.MoveUp();
        k.Insert(new DigitNode("4"));
        k.MoveDown();
        Expect.Latex("3▦^{4}", k);
    }

    [Fact]
    public void Poewr_can_be_left_empty__moving_out_and_back_in()
    {
        var k = new KeyboardMemory();
        k.Insert(new AscendingBranchingNode("", "^{", "}"));
        Expect.Latex("▦^{⬚}", k);
        k.MoveLeft();
        Expect.Latex("▦⬚^{⬚}", k);
        k.MoveRight();
        Expect.Latex("▦^{⬚}", k);
    }

    [Fact]
    public void Impossible_updown_requests_in_an_empty_power_should_not_throw()
    {
        var k = new KeyboardMemory();
        k.Insert(new AscendingBranchingNode("", "^{", "}"));
        Expect.Latex("▦^{⬚}", k);
        k.MoveDown();
        Expect.Latex("▦^{⬚}", k);
        k.MoveUp();
        Expect.Latex("⬚^{▦}", k);
        k.MoveUp();
        Expect.Latex("⬚^{▦}", k);
    }

    [Fact]
    public void Impossible_updown_requests_in_a_filled_power_should_not_throw()
    {
        var k = new KeyboardMemory();
        k.Insert(new AscendingBranchingNode("", "^{", "}"));
        k.Insert(new DigitNode("3"));
        Expect.Latex("3▦^{⬚}", k);
        k.MoveDown();
        Expect.Latex("3▦^{⬚}", k);
        k.MoveUp();
        Expect.Latex("3^{▦}", k);
        k.Insert(new DigitNode("4"));
        Expect.Latex("3^{4▦}", k);
        k.MoveUp();
        Expect.Latex("3^{4▦}", k);
    }
}
