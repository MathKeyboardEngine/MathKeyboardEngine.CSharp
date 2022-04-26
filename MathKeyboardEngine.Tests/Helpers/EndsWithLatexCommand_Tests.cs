using Xunit;

namespace MathKeyboardEngine.Tests;
public class EndsWithLatexCommand_Tests
{
    [Theory]
    [InlineData(@"\pi")]
    [InlineData(@"2\pi")]
    [InlineData(@"2\times\pi")]
    [InlineData(@"\sin")]
    public void True(string s)
    {
        Assert.True(s.EndsWithLatexCommand());
    }

    [Theory]
    [InlineData(@"\pi^2")]
    [InlineData(@"\sin6")]
    [InlineData(@"\sin a")]
    [InlineData(@"")]
    [InlineData(@"\|")]
    [InlineData(@"\\")]
    public void False(string s)
    {
        Assert.False(s.EndsWithLatexCommand());
    }
}
