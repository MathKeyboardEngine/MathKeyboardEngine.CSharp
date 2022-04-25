namespace MathKeyboardEngine
{
    public class DigitNode : PartOfNumberWithDigits
    {
        private readonly string _latex;

        public DigitNode(string latex)
        {
            _latex = latex;
        }
        protected override string GetLatexPart(KeyboardMemory k, LatexConfiguration latexConfiguration)
        {
            return _latex;
        }
    }
}
