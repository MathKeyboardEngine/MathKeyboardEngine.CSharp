using Xunit;

namespace MathKeyboardEngine.Tests;
public class RoundBracketsNode_Tests
{
    [Fact]
    public void Default_bracket_style()
    {
        var k = new KeyboardMemory();
        k.Insert(new RoundBracketsNode());
        Expect.Latex(@"\left(▦\right)", k);
    }

    [Fact]
    public void Bracket_style_can_be_overridden()
    {
        var k = new KeyboardMemory();
        k.Insert(new RoundBracketsNode("(", ")"));
        Expect.Latex("(▦)", k);
    }
}
