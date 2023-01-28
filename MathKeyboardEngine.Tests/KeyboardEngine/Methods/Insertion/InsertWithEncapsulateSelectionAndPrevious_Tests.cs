using System;
using Xunit;

namespace MathKeyboardEngine.Tests;

public class InsertWithEncapsulateSelectionAndPrevious_Tests
{
    [Fact]
    public void When_a_single_TreeNode_is_selected_and_the_exclusive_left_border_is_a_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("2"));
        k.Insert(new DigitNode("3"));
        Expect.Latex("23▦", k);
        k.SelectLeft();
        Expect.Latex(@"2\colorbox{blue}{3}", k);
        // Act
        k.InsertWithEncapsulateSelectionAndPrevious(new AscendingBranchingNode("", "^{", "}"));
        // Assert
        Expect.Latex(@"2^{3▦}", k);
    }

    [Fact]
    public void When_a_single_TreeNode_is_selected_and_the_exclusive_left_border_is_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("2"));
        Expect.Latex("2▦", k);
        k.SelectLeft();
        Expect.Latex(@"\colorbox{blue}{2}", k);
        // Act
        k.InsertWithEncapsulateSelectionAndPrevious(new AscendingBranchingNode("", "^{", "}"));
        // Assert
        Expect.Latex(@"⬚^{2▦}", k);
    }

    [Fact]
    public void When_multiple_TreeNodes_are_selected_and_the_exclusive_left_border_is_a_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("2"));
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("0"));
        Expect.Latex("210▦", k);
        k.SelectLeft();
        k.SelectLeft();
        Expect.Latex(@"2\colorbox{blue}{10}", k);
        // Act
        k.InsertWithEncapsulateSelectionAndPrevious(new AscendingBranchingNode("", "^{", "}"));
        // Assert
        Expect.Latex(@"2^{10▦}", k);
    }

    [Fact]
    public void When_multiple_TreeNodes_are_selected_and_the_exclusive_left_border_is_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        Expect.Latex("12▦", k);
        k.SelectLeft();
        k.SelectLeft();
        Expect.Latex(@"\colorbox{blue}{12}", k);
        // Act
        k.InsertWithEncapsulateSelectionAndPrevious(new AscendingBranchingNode("", "^{", "}"));
        // Assert
        Expect.Latex(@"⬚^{12▦}", k);
    }

    [Fact]
    public void Invokes_InsertWithEncapsulateCurrent_if_InSelectionMode_but_nothing_selected()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new StandardLeafNode("+"));
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.EnterSelectionMode();
        Expect.Latex("1+12▦", k);
        // Act
        k.InsertWithEncapsulateSelectionAndPrevious(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        // Assert
        Expect.Latex(@"1+\frac{12}{▦}", k);
    }

    [Fact]
    public void Throws_on_inserting_BranchingNode_with_single_Placeholder()
    {
        var k = new KeyboardMemory();
        var ex = Assert.Throws<Exception>(() => k.InsertWithEncapsulateSelectionAndPrevious(new StandardBranchingNode("[", "]")));
        Assert.Equal("Expected 2 Placeholders.", ex.Message);
    }
}
