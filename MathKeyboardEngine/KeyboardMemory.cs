namespace MathKeyboardEngine
{
    public class KeyboardMemory
    {
        public KeyboardMemory()
        {
            SyntaxTreeRoot = new();
            Current = SyntaxTreeRoot;
        }
        public Placeholder SyntaxTreeRoot { get; }
        public SyntaxTreeComponent Current { get; set; }

        public int? SelectionDiff { get; set; }
        public TreeNode? InclusiveSelectionRightBorder { get; set; }
        public SyntaxTreeComponent? InclusiveSelectionLeftBorder { get; set; }
    }
}