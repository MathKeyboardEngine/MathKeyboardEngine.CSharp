using Xunit;
using MathKeyboardEngine._Helpers;

namespace MathKeyboardEngine.Tests;

public class GetBracketPairContent_Tests
{
    [Theory]
    [InlineData(@"\frac{", "}", @"\frac{1}{2}", "1", "{2}")]
    [InlineData(@"\frac{", "}", @"\frac{123}{456}", "123", "{456}")]
    [InlineData(@"\frac{", "}", @"\frac{\frac{1}{1-x}}{x}", @"\frac{1}{1-x}", "{x}")]
    [InlineData(@"\frac{", "}", @"\frac{TEST\right}and\}FORFUN}{x}", @"TEST\right}and\}FORFUN", "{x}")]
    [InlineData(@"\frac{", "}", @"\frac{1}{2}3", "1", "{2}3")]
    public void Test(string opening, string closingBracket, string sWithOpening, string expectedContent, string expectedRest)
    {
      // Act
      var result = sWithOpening.GetBracketPairContent(opening, closingBracket);
      // Assert
      Assert.Equal(expectedContent, result.Content);
      Assert.Equal(expectedRest, result.Rest);
  }
}
