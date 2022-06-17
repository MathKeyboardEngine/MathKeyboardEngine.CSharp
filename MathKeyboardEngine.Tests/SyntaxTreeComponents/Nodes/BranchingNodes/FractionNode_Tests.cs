using Xunit;

namespace MathKeyboardEngine.Tests;

public class FractionNode_Tests
{
    [Fact]
    public void Frac_left_right_right_right()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.MoveLeft();
        Expect.Latex(@"▦\frac{⬚}{⬚}", k);
        k.MoveRight();
        Expect.Latex(@"\frac{▦}{⬚}", k);
        k.MoveRight();
        Expect.Latex(@"\frac{⬚}{▦}", k);
        k.MoveRight();
        Expect.Latex(@"\frac{⬚}{⬚}▦", k);
    }

    [Fact]
    public void Frac_3_right_4()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        k.Insert(new DigitNode("4"));
        Expect.Latex(@"\frac{3}{4▦}", k);
    }

    [Fact]
    public void Frac_3_down_4()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.Insert(new DigitNode("3"));
        k.MoveDown();
        k.Insert(new DigitNode("4"));
        Expect.Latex(@"\frac{3}{4▦}", k);
    }

    [Fact]
    public void Three_encapsulated_by_the_numerator_of_a_fraction()
    {
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("3"));
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        Expect.Latex(@"\frac{3}{▦}", k);
    }

    [Fact]
    public void Delete_empty_fraction_from_numerator()
    {
        var k = new KeyboardMemory();
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        Expect.Latex(@"\frac{▦}{⬚}", k);
        k.DeleteCurrent();
        Expect.Latex("▦", k);
    }

    [Fact]
    public void Delete_empty_fraction_from_denominator()
    {
        var k = new KeyboardMemory();
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.MoveDown();
        Expect.Latex(@"\frac{⬚}{▦}", k);
        k.DeleteCurrent();
        Expect.Latex("▦", k);
    }

    // continue port here
}
