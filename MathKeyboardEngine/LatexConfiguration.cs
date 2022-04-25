namespace MathKeyboardEngine
{
    public class LatexConfiguration
    {
        public string ActivePlaceholderShape { get; set; } = @"\blacksquare";
        public string? ActivePlaceholderColor { get; set; }
        public string PassivePlaceholderShape { get; set; } = @"\square";
        public string? PassivePlaceholderColor { get; set; }
        public string SelectionHightlightStart { get; set; } = @"\colorbox{#ADD8E6}{";
        public string SelectionHightlightEnd { get; set; } = @"}";

        public string ActivePlaceholderLatex
        {
            get
            {
                if (ActivePlaceholderColor == null)
                {
                    return ActivePlaceholderShape;
                }
                else
                {
                    return @"\color{" + ActivePlaceholderColor + "}{" + ActivePlaceholderShape + "}";
                }
            }
        }

        public string PassivePlaceholderLatex
        {
            get
            {
                if (PassivePlaceholderColor == null)
                {
                    return PassivePlaceholderShape;
                }
                else
                {
                    return @"\color{" + PassivePlaceholderColor + "}{" + PassivePlaceholderShape + "}";
                }
            }
        }
    }
}
