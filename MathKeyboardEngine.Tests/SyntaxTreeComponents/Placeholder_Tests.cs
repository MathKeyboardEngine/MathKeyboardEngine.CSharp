using Xunit;

namespace MathKeyboardEngine.Tests;
public class Placeholder_Tests
{
    [Theory]
    [InlineData(@"\sin", @"a", @"\sin a")]
    [InlineData(@"\sin", @"2", @"\sin2")]
    [InlineData(@"2", @"\pi", @"2\pi")]
    [InlineData(@"a", @"\pi", @"a\pi")]
    [InlineData(@"\alpha", @"\pi", @"\alpha\pi")]
    public void The_minimum_amount_of_required_space_is_added_by_Placeholder_GetLatex(string node1, string node2, string expectedLatexOutput)
    {
        var k = new KeyboardMemory();
        k.Insert(new StandardLeafNode(node1));
        k.Insert(new StandardLeafNode(node2));
        Expect.ViewModeLatex(expectedLatexOutput, k);
    }
}
