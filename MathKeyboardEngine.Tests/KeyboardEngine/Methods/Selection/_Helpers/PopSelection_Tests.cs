using System;
using MathKeyboardEngine.__Helpers;
using Xunit;

namespace MathKeyboardEngine.Tests;

public class PopSelection_Tests
{
    [Fact]
    public void Throws_if_not_InSelectionMode()
    {
        var k = new KeyboardMemory();
        var ex = Assert.Throws<Exception>(() => k.PopSelection());
        Assert.Equal("Enter selection mode before calling this method.", ex.Message);
    }

    [Fact]
    public void Returns_an_empty_List_when_InSelectionMode_but_nothing_is_selected()
    {
        var k = new KeyboardMemory();
        k.EnterSelectionMode();
        var list = k.PopSelection();
        Assert.Empty(list);
    }
}
