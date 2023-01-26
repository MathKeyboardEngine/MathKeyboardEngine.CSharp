namespace MathKeyboardEngine._Helpers;
public static class Concat
{
    public static string Latex(params string[] latexParts)
    {
        return latexParts.ConcatLatex();
    }

    public static string ConcatLatex(this IEnumerable<string> latexParts)
    {
        var s = "";
        foreach (var part in latexParts)
        {
            if (s.EndsWithLatexCommand() && char.IsLetter(part[0]))
            {
                s += " ";
            }
            s += part;
        }
        return s;
    }
}
