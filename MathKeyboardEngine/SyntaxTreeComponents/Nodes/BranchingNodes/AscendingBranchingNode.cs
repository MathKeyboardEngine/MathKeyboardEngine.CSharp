namespace MathKeyboardEngine;

public class AscendingBranchingNode(string before, string then, params string[] rest) : StandardBranchingNode(before, then, rest)
{
    public override Placeholder? GetMoveDownSuggestion(Placeholder fromPlaceholder)
    {
        var currentPlaceholderIndex = Placeholders.IndexOf(fromPlaceholder);
        if (currentPlaceholderIndex > 0)
        {
            return Placeholders[currentPlaceholderIndex - 1];
        }
        else
        {
            return null;
        }
    }

    public override Placeholder? GetMoveUpSuggestion(Placeholder fromPlaceholder)
    {
        var currentPlaceholderIndex = Placeholders.IndexOf(fromPlaceholder);
        if (currentPlaceholderIndex < Placeholders.Count - 1)
        {
            return Placeholders[currentPlaceholderIndex + 1];
        }
        else
        {
            return null;
        }
    }
}
