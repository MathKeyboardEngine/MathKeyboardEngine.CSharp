using MathKeyboardEngine.__Helpers;

namespace MathKeyboardEngine;

public static class Parse
{
    public static KeyboardMemory Latex(string? latex, LatexParserConfiguration? latexParserConfiguration = null)
    {
        latexParserConfiguration ??= new LatexParserConfiguration();
        if (latex == null)
        {
            return new KeyboardMemory();
        }
        var x = latex.Trim();

        var k = new KeyboardMemory();

        while (x != "")
        {
            if (x[0] == ' ')
            {
                x = x.TrimStart();
                continue;
            }

            var decimalSeparatorMatch = latexParserConfiguration.DecimalSeparatorMatchers.FirstOrDefault(pattern => x.StartsWith(pattern));
            if (decimalSeparatorMatch != null)
            {
                k.Insert(new DecimalSeparatorNode(latexParserConfiguration.PreferredDecimalSeparator ?? (() => decimalSeparatorMatch)));
                x = x[decimalSeparatorMatch.Length..];
                continue;
            }

            if (char.IsDigit(x[0]) || latexParserConfiguration.AdditionalDigits?.Contains(x[0].ToString()) == true)
            {
                k.Insert(new DigitNode(x[0].ToString()));
                x = x[1..];
                continue;
            }

            var handled = false;

            if (x.StartsWith(@"\begin{"))
            {
                var matrixTypeAndRest = x.GetBracketPairContent(@"\begin{", "}");
                if (!matrixTypeAndRest.Content.EndsWith("matrix") && !matrixTypeAndRest.Content.EndsWith("cases"))
                {
                    throw new Exception("""Expected a word ending with "matrix" or "cases" after "\begin{".""");
                }
                var matrixContent = matrixTypeAndRest.Rest[..matrixTypeAndRest.Rest.IndexOf(@$"\end{{{matrixTypeAndRest.Content}}}")];
                var lines = matrixContent.Split(@"\\");
                k.Insert(new MatrixNode(matrixTypeAndRest.Content, lines[0].Split("&").Length, lines.Length));
                foreach (var line in lines)
                {
                    foreach (var elementLatex in line.Split("&"))
                    {
                        var nodes = Parse.Latex(elementLatex, latexParserConfiguration).SyntaxTreeRoot.Nodes;
                        k.Insert(nodes);
                        k.MoveRight();
                    }
                }
                var matrixEnd = @$"\end{{{matrixTypeAndRest.Content}}}";
                x = x[(x.IndexOf(matrixEnd) + matrixEnd.Length)..];
                continue;
            }

            if (latexParserConfiguration.PreferRoundBracketsNode && (x[0] == '(' || x.StartsWith(@"\left(")))
            {
                var opening = x[0] == '(' ? "(" : @"\left(";
                var closing = x[0] == '(' ? ")" : @"\right)";
                var bracketsNode = new RoundBracketsNode(opening, closing);
                k.Insert(bracketsNode);
                var bracketsContentAndRest = x.GetBracketPairContent(opening, closing);
                var bracketsContentNodes = Parse.Latex(bracketsContentAndRest.Content, latexParserConfiguration).SyntaxTreeRoot.Nodes;
                k.Insert(bracketsContentNodes);
                k.Current = bracketsNode;
                x = bracketsContentAndRest.Rest;
                continue;
            }

            if (x.StartsWith(@"\"))
            {
                foreach (var prefix in new[] { @"\left\", @"\right\", @"\left", @"\right" })
                {
                    if (x.StartsWith(prefix) && !char.IsLetter(x[prefix.Length]))
                    {
                        k.Insert(new StandardLeafNode(prefix + x[prefix.Length]));
                        x = x[(prefix.Length + 1)..];
                        handled = true;
                        break;
                    }
                }
                if (handled)
                {
                    continue;
                }

                var textOpening = @"\text{";
                if (x.StartsWith(textOpening))
                {
                    var bracketPairContentAndRest = x.GetBracketPairContent(textOpening, "}");
                    var textNode = new StandardBranchingNode(textOpening, "}");
                    k.Insert(textNode);
                    foreach (var character in bracketPairContentAndRest.Content)
                    {
                        k.Insert(new StandardLeafNode(character.ToString()));
                    }
                    k.Current = textNode;
                    x = bracketPairContentAndRest.Rest;
                    continue;
                }

                var command = @"\";
                if (char.IsLetter(x[1]))
                {
                    for (var i = 1; i < x.Length; i++)
                    {
                        var character = x[i];
                        if (char.IsLetter(character))
                        {
                            command += character;
                        }
                        else if (character == '{' || character == '[')
                        {
                            var opening = command + character;
                            var closingBracket1 = character == '{' ? "}" : "]";
                            var bracketPair1ContentAndRest = x.GetBracketPairContent(opening, closingBracket1);
                            var placeholder1Nodes = Parse.Latex(bracketPair1ContentAndRest.Content, latexParserConfiguration).SyntaxTreeRoot.Nodes;
                            if (bracketPair1ContentAndRest.Rest.FirstOrDefault() == '{')
                            {
                                var multiPlaceholderBranchingNode = new DescendingBranchingNode(opening, closingBracket1 + "{", "}");
                                k.Insert(multiPlaceholderBranchingNode);
                                k.Insert(placeholder1Nodes);
                                k.MoveRight();
                                var bracketPair2ContentAndRest = bracketPair1ContentAndRest.Rest.GetBracketPairContent("{", "}");
                                var placeholder2Nodes = Parse.Latex(bracketPair2ContentAndRest.Content, latexParserConfiguration).SyntaxTreeRoot.Nodes;
                                k.Insert(placeholder2Nodes);
                                k.Current = multiPlaceholderBranchingNode;
                                x = bracketPair2ContentAndRest.Rest;
                            }
                            else
                            {
                                var singlePlaceholderBranchingNode = new StandardBranchingNode(opening, closingBracket1.ToString());
                                k.Insert(singlePlaceholderBranchingNode);
                                k.Insert(placeholder1Nodes);
                                k.Current = singlePlaceholderBranchingNode;
                                x = bracketPair1ContentAndRest.Rest;
                            }
                            handled = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (handled)
                    {
                        continue;
                    }
                    k.Insert(new StandardLeafNode(command));
                    x = x[command.Length..];
                }
                else
                {
                    k.Insert(new StandardLeafNode(@"\" + x[1]));
                    x = x[2..];
                }
                continue;
            }

            if (x.StartsWith("_{"))
            {
                var opening = "_{";
                var closingBracket1 = "}";
                var bracketPair1ContentAndRest = x.GetBracketPairContent(opening, closingBracket1);
                if (bracketPair1ContentAndRest.Rest.StartsWith("^{"))
                {
                    var ascendingBranchingNode = new AscendingBranchingNode(opening, "}^{", "}");
                    k.Insert(ascendingBranchingNode);
                    var placeholder1Nodes = Parse.Latex(bracketPair1ContentAndRest.Content, latexParserConfiguration).SyntaxTreeRoot.Nodes;
                    k.Insert(placeholder1Nodes);
                    k.MoveRight();
                    var bracketPair2ContentAndRest = bracketPair1ContentAndRest.Rest.GetBracketPairContent("^{", "}");
                    var placeholder2Nodes = Parse.Latex(bracketPair2ContentAndRest.Content, latexParserConfiguration).SyntaxTreeRoot.Nodes;
                    k.Insert(placeholder2Nodes);
                    k.Current = ascendingBranchingNode;
                    x = bracketPair2ContentAndRest.Rest;
                    continue;
                }
            }

            var various = new (string Opening, Func<BranchingNode> GetTreeNode)[]
            {
                  new ("^{", () => new AscendingBranchingNode("", "^{", "}")),
                  new ("_{", () => new DescendingBranchingNode("", "_{", "}"))
            };
            foreach (var opening_getTreeNode in various)
            {
                var opening = opening_getTreeNode.Opening;
                if (x.StartsWith(opening))
                {
                    var node = opening_getTreeNode.GetTreeNode();
                    k.InsertWithEncapsulateCurrent(node);
                    var bracketPairContentAndRest = x.GetBracketPairContent(opening, "}");
                    var secondPlaceholderNodes = Parse.Latex(bracketPairContentAndRest.Content, latexParserConfiguration).SyntaxTreeRoot.Nodes;
                    k.Insert(secondPlaceholderNodes);
                    k.Current = node;
                    x = bracketPairContentAndRest.Rest;
                    handled = true;
                    break;
                }
            }
            if (handled)
            {
                continue;
            }

            k.Insert(new StandardLeafNode(x[0].ToString()));
            x = x[1..];
            continue;

        }
        return k;
    }
}
