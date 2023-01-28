﻿using MathKeyboardEngine._Helpers;

namespace MathKeyboardEngine;

public static class _InsertWithEncapsulateSelectionAndPrevious
{
    public static void InsertWithEncapsulateSelectionAndPrevious(this KeyboardMemory k, BranchingNode newNode)
    {
        if (newNode.Placeholders.Count < 2)
        {
            throw new Exception($"Expected 2 {nameof(Placeholder)}s.");
        }
        var selection = k.PopSelection();
        var secondPlaceholder = newNode.Placeholders[1];
        secondPlaceholder.Encapsulate(selection);
        k.InsertWithEncapsulateCurrent(newNode);
        k.Current = selection.LastOrDefault() ?? (SyntaxTreeComponent)secondPlaceholder;
    }
}
