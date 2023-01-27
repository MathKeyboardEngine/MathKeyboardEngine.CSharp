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
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        Expect.Latex(@"\frac{▦}{⬚}", k);
        k.DeleteLeft();
        Expect.Latex("▦", k);
    }

    [Fact]
    public void Delete_empty_fraction_from_denominator()
    {
        var k = new KeyboardMemory();
        k.InsertWithEncapsulateCurrent(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.MoveDown();
        Expect.Latex(@"\frac{⬚}{▦}", k);
        k.DeleteLeft();
        Expect.Latex("▦", k);
    }

    [Fact]
    public void Delete_empty_fraction_from_right()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.MoveDown();
        k.MoveRight();
        Expect.Latex(@"\frac{⬚}{⬚}▦", k);
        k.DeleteLeft();
        Expect.Latex("▦", k);
    }

    [Fact]
    public void Deleting_fraction_from_denominator_releases_nonempty_numerator()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.MoveDown();
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        Expect.Latex(@"\frac{12}{3}▦", k);
        k.DeleteLeft();
        Expect.Latex(@"\frac{12}{▦}", k);
        k.DeleteLeft();
        Expect.Latex("12▦", k);
    }

    [Fact]
    public void MoveUp_in_filled_fraction()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.MoveDown();
        k.Insert(new DigitNode("3"));
        Expect.Latex(@"\frac{12}{3▦}", k);
        k.MoveUp();
        Expect.Latex(@"\frac{12▦}{3}", k);
    }

    [Fact]
    public void Impossible_updown_requests_in_filled_fraction_should_not_throw()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.Insert(new DigitNode("1"));
        Expect.Latex(@"\frac{1▦}{⬚}", k);
        k.MoveUp();
        Expect.Latex(@"\frac{1▦}{⬚}", k);

        k.MoveDown();
        k.Insert(new DigitNode("2"));
        Expect.Latex(@"\frac{1}{2▦}", k);
        k.MoveDown();
        Expect.Latex(@"\frac{1}{2▦}", k);
    }

    [Fact]
    public void Impossible_updown_requests_in_empty_fraction_should_not_throw()
    {
        var k = new KeyboardMemory();
        k.Insert(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        k.MoveDown();
        Expect.Latex(@"\frac{⬚}{▦}", k);
        k.MoveDown();
        Expect.Latex(@"\frac{⬚}{▦}", k);
        k.MoveUp();
        Expect.Latex(@"\frac{▦}{⬚}", k);
        k.MoveUp();
        Expect.Latex(@"\frac{▦}{⬚}", k);
    }
}
