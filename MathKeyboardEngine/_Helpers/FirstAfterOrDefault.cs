namespace MathKeyboardEngine._Helpers;

public static class _FirstAfterOrDefault
{
    public static T? FirstAfterOrDefault<T>(this IEnumerable<T> source, T element) where T : class
    {
        return source.SkipWhile(x => x != element).Skip(1).FirstOrDefault();
    }
}
