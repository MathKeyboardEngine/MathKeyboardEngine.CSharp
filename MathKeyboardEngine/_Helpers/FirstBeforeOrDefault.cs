namespace MathKeyboardEngine._Helpers;
public static class _FirstBeforeOrDefault
{
    public static T? FirstBeforeOrDefault<T>(this IEnumerable<T> source, T element) where T : class
    {
        return source.Reverse().FirstAfterOrDefault(element);
    }
}
