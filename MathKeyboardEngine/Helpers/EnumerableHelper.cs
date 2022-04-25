namespace MathKeyboardEngine
{
    public static class EnumerableHelper
    {
        public static T? FirstAfterOrDefault<T>(this IEnumerable<T> source, T element) where T : class
        {
            return source.SkipWhile(x => x != element).Skip(1).FirstOrDefault();
        }

        public static T? FirstBeforeOrDefault<T>(this IEnumerable<T> source, T element) where T : class
        {
            return source.Reverse().FirstAfterOrDefault(element);
        }
    }
}
