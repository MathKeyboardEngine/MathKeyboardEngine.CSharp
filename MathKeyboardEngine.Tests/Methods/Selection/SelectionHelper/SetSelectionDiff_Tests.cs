using System;
using Xunit;

namespace MathKeyboardEngine.Tests;
public class SetSelectionDiff_Tests
{
    [Fact]
    public void Throws_at_nonsensical_requests()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("1"));
        k.SelectLeft();
        Expect.Latex(@"\colorbox{blue}{1}", k);
        // Act & Assert
        var ex = Assert.Throws<Exception>(() => SelectionHelper.SetSelectionDiff(k, k.SelectionDiff!.Value - 1)); // Trying to go even more to the left.
        Assert.Equal($"The {nameof(TreeNode)} at index 0 of the current {nameof(Placeholder)} is as far as you can go left if current is a {nameof(TreeNode)}.", ex.Message);
    }
}
