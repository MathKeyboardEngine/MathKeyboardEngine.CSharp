namespace MathKeyboardEngine
{
    public class RoundBracketsNode : StandardBranchingNode
    {
        public RoundBracketsNode(string leftBracketLatex = @"\left(", string rightBracketLatex = @"\right)")
            : base(leftBracketLatex, rightBracketLatex)
        {

        }
    }
}
