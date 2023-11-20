using Xunit;

namespace MathKeyboardEngine.Tests;

public class KeyboardMemory_SyntaxTreeRoot_Tests
{
    [Fact]
    public void SyntaxTreeRoot_is_equal_to_Current_on_KeyboardMemory_initialization()
    {
        var k = new KeyboardMemory();
        Assert.NotNull(k.SyntaxTreeRoot);
        Assert.Equal(k.Current, k.SyntaxTreeRoot);
    }

    [Fact]
    public void SyntaxTreeRoot_is_a_Placeholder()
    {
        var k = new KeyboardMemory();
        Assert.True(k.SyntaxTreeRoot is Placeholder);
    }

    [Fact]
    public void SyntaxTreeRoot_cannot_be_deleted()
    {
        var k = new KeyboardMemory();
        k.DeleteLeft();
        Assert.True(k.SyntaxTreeRoot is Placeholder);
    }

    [Fact]
    public void SyntaxTreeRoot_is_reachable_via_the_chain_of_parents()
    {
        var k = new KeyboardMemory();

        var fraction1 = new DescendingBranchingNode(@"\frac{", "}{", "}");
        k.Insert(fraction1);
        Assert.True(k.Current == fraction1.Placeholders[0]);

        var fraction2 = new DescendingBranchingNode(@"\frac{", "}{", "}");
        k.Insert(fraction2);
        Assert.True(k.Current == fraction2.Placeholders[0]);

        var calcualtedRoot = ((Placeholder)k.Current).ParentNode!.ParentPlaceholder.ParentNode!.ParentPlaceholder;
        Assert.Null(calcualtedRoot.ParentNode);
        Assert.Equal(k.SyntaxTreeRoot, calcualtedRoot);
    }

    [Fact]
    public void Impossible_move_requests_in_an_empty_root_Placeholder_do_not_throw()
    {
        var k = new KeyboardMemory();
        Expect.EditModeLatex("▦", k);
        k.MoveLeft();
        Expect.EditModeLatex("▦", k);
        k.MoveDown();
        Expect.EditModeLatex("▦", k);
        k.MoveUp();
        Expect.EditModeLatex("▦", k);
        k.MoveRight();
        Expect.EditModeLatex("▦", k);
    }

    [Fact]
    public void Impossible_move_requests_in_a_filled_root_Placeholder_do_not_throw()
    {
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        Expect.EditModeLatex("1▦", k);
        k.MoveUp();
        Expect.EditModeLatex("1▦", k);
        k.MoveRight();
        Expect.EditModeLatex("1▦", k);
        k.MoveDown();
        Expect.EditModeLatex("1▦", k);
        k.MoveLeft();
        Expect.EditModeLatex("▦1", k);
        k.MoveDown();
        Expect.EditModeLatex("▦1", k);
        k.MoveLeft();
        Expect.EditModeLatex("▦1", k);
        k.MoveUp();
        Expect.EditModeLatex("▦1", k);
    }
}
