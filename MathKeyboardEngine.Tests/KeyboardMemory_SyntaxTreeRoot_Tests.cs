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
        k.DeleteCurrent();
        Assert.True(k.SyntaxTreeRoot is Placeholder);
    }

    // Continue port here.
}
