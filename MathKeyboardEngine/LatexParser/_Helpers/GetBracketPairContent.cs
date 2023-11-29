namespace MathKeyboardEngine.__Helpers;

public record BracePairContent(string Content, string Rest);

public static class __GetBracketPairContent
{
    public static BracePairContent GetBracketPairContent(this string sWithOpening, string opening, string closing)
    {
        var openingBracket = opening.Last();
        var s = sWithOpening[opening.Length..];
        var level = 0;
        for (var closingBracketIndex = 0; closingBracketIndex < s.Length; closingBracketIndex++)
        {
            if (s.Substring(closingBracketIndex, closing.Length) == closing)
            {
                if (level == 0)
                {
                    return new BracePairContent(Content: s[..closingBracketIndex], Rest: s[(closingBracketIndex + closing.Length)..]);
                }
                else
                {
                    level--;
                    continue;
                }
            }

            var toIgnores = new string[] { @"\" + openingBracket, @"\" + closing, @"\left" + openingBracket, @"\right" + closing };
            var currentPosition = s[closingBracketIndex..];
            foreach (var toIgnore in toIgnores)
            {
                if (currentPosition.Length >= toIgnore.Length && currentPosition.StartsWith(toIgnore))
                {
                    closingBracketIndex += toIgnore.Length;
                    continue;
                }
            }

            if (s[closingBracketIndex] == openingBracket)
            {
                level++;
            }
        }
        throw new Exception($"A closing {closing} is missing.");
    }
}
