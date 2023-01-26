namespace MathKeyboardEngine._Helpers;
public static class _EndsWithLatexCommand
{ 
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
