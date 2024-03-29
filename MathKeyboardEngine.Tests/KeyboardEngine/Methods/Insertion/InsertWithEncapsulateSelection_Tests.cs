﻿using Xunit;

namespace MathKeyboardEngine.Tests;

public class InsertWithEncapsulateSelection_Tests
{
    [Fact]
    public void When_a_single_TreeNode_is_selected_and_the_exclusive_left_border_is_a_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        Expect.EditModeLatex("12▦", k);
        k.SelectLeft();
        Expect.EditModeLatex(@"1\colorbox{blue}{2}", k);
        // Act
        k.InsertWithEncapsulateSelection(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        // Assert
        Expect.EditModeLatex(@"1\frac{2}{▦}", k);
    }

    [Fact]
    public void When_a_single_TreeNode_is_selected_and_the_exclusive_left_border_is_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        Expect.EditModeLatex("1▦", k);
        k.SelectLeft();
        Expect.EditModeLatex(@"\colorbox{blue}{1}", k);
        // Act
        k.InsertWithEncapsulateSelection(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        // Assert
        Expect.EditModeLatex(@"\frac{1}{▦}", k);
    }

    [Fact]
    public void When_multiple_TreeNodes_are_selected_and_the_exclusive_left_border_is_a_TreeNode()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.Insert(new DigitNode("3"));
        Expect.EditModeLatex("123▦", k);
        k.SelectLeft();
        k.SelectLeft();
        Expect.EditModeLatex(@"1\colorbox{blue}{23}", k);
        // Act
        k.InsertWithEncapsulateSelection(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        // Assert
        Expect.EditModeLatex(@"1\frac{23}{▦}", k);
    }

    [Fact]
    public void When_multiple_TreeNodes_are_selected_and_the_exclusive_left_border_is_a_Placeholder()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        Expect.EditModeLatex("12▦", k);
        k.SelectLeft();
        k.SelectLeft();
        Expect.EditModeLatex(@"\colorbox{blue}{12}", k);
        // Act
        k.InsertWithEncapsulateSelection(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        // Assert
        Expect.EditModeLatex(@"\frac{12}{▦}", k);
    }


    [Fact]
    public void Does_a_regular_Insert_when_InSelectionMode_but_nothing_is_selected()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.Insert(new DigitNode("2"));
        k.EnterSelectionMode();
        Expect.EditModeLatex("12▦", k);
        // Act
        k.InsertWithEncapsulateSelection(new DescendingBranchingNode(@"\frac{", "}{", "}"));
        // Assert
        Expect.EditModeLatex(@"12\frac{▦}{⬚}", k);
    }
}
