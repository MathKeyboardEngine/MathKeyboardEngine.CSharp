namespace MathKeyboardEngine.__Helpers;

public static class __FirstBeforeOrDefault
{
    public static T? FirstBeforeOrDefault<T>(this IEnumerable<T> source, T element) where T : class
    {
        return source.Reverse().FirstAfterOrDefault(element);
    }
}
