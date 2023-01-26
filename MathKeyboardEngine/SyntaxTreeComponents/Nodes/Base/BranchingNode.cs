namespace MathKeyboardEngine;
public abstract class BranchingNode : TreeNode
{
    public BranchingNode(List<Placeholder> placeholders)
    {
        Placeholders = placeholders;
        foreach (var placeholder in placeholders)
        {
            placeholder.ParentNode = this;
        }
    }

    public List<Placeholder> Placeholders { get; }

    public virtual Placeholder? GetMoveDownSuggestion(Placeholder fromPlaceholder)
    {
        return null;
    }

    public virtual Placeholder? GetMoveUpSuggestion(Placeholder fromPlaceholder)
    {
        return null;
    }
}
