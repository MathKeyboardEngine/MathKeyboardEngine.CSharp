using System.Collections.Generic;
using Xunit;

namespace MathKeyboardEngine.Tests;

public class BranchingNode_Tests
{
    [Fact]
    public void Calling_MoveUp_or_MoveDown_does_not_throw_even_if_not_implemented()
    {
        var k = new KeyboardMemory();
        k.Insert(new DummyBranchingNode());
        Expect.EditModeLatex(@"wow >> ▦ << wow", k);
        k.MoveUp();
        Expect.EditModeLatex(@"wow >> ▦ << wow", k);
        k.MoveDown();
        Expect.EditModeLatex(@"wow >> ▦ << wow", k);
    }

    public class DummyBranchingNode : BranchingNode
    {
        public DummyBranchingNode() : base(new List<Placeholder> { new() })
        {
        }

        protected override string GetLatexPart(KeyboardMemory k, LatexConfiguration latexConfiguration)
        {
            return $"wow >> {Placeholders[0].GetLatex(k, latexConfiguration)} << wow";
        }
    }
}
