namespace MathKeyboardEngine
{
    public static class StringHelper
    {
        public static string ConcatLatex(params string[] latexParts)
        {
            return latexParts.ConcatLatex();
        }

        public static string ConcatLatex(this IEnumerable<string> latexParts)
        {
            string? s = "";
            foreach (string? part in latexParts)
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
                for (int i = latex.Length - 2; i >= 0; i--)
                {
                    char c = latex[i];
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
}
