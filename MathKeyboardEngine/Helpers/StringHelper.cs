namespace MathKeyboardEngine;

public static class StringHelper
{
    public static string ConcatLatex(params string[] latexParts)
    {
        return latexParts.ConcatLatex();
    }

    public static string ConcatLatex(this IEnumerable<string> latexParts)
    {
        var s = "";
        foreach (var part in latexParts)
        {
            if (EndsWithLatexCommand(s) && char.IsLetter(part[0]))
            {
                s += " ";
            }
            s += part;
        }
        return s;
    }

    public static bool EndsWithLatexCommand(this string latex)
    {
        if (latex.Length == 0)
        {
            return false;
        }

        if (char.IsLetter(latex[latex.Length - 1]))
        {
            for (var i = latex.Length - 2; i >= 0; i--)
            {
                var c = latex[i];
                if (char.IsLetter(c))
                {
                    continue;
                }
                else
                {
                    return c == '\\';
                }
            }
        }
        return false;
    }
}
