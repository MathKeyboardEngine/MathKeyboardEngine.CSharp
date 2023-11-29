namespace MathKeyboardEngine.__Helpers;

public static class __GetFirstNonEmptyOnLeftOf
{
    public static Placeholder? GetFirstNonEmptyOnLeftOf(this IEnumerable<Placeholder> source, Placeholder element)
    {
        return source.Reverse().SkipWhile(x => x != element).Skip(1).FirstOrDefault(x => x.Nodes.Count > 0);
    }
}
