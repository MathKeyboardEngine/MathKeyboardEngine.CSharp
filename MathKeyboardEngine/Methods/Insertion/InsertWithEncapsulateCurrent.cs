﻿namespace MathKeyboardEngine;

public class InsertWithEncapsulateCurrentMethod
{
    public static void InsertWithEncapsulateCurrent(KeyboardMemory k, BranchingNode newNode, Options? options = null)
    {
        var encapsulatingPlaceholder = newNode.Placeholders[0];
        if (k.Current is TreeNode)
        {
            var current = (TreeNode)k.Current;
            var siblingNodes = current.ParentPlaceholder.Nodes;
            var currentIndex = siblingNodes.IndexOf(current);
            siblingNodes[currentIndex] = newNode;
            newNode.ParentPlaceholder = current.ParentPlaceholder;
            if (current is RoundBracketsNode b && options == Options.DeleteOuterRoundBracketsIfAny)
            {
                encapsulatingPlaceholder.Encapsulate(b.Placeholders[0].Nodes);
                k.Current = newNode.Placeholders.FirstAfterOrDefault(encapsulatingPlaceholder) ?? (SyntaxTreeComponent)newNode;
            }
            else if (current is PartOfNumberWithDigits)
            {
                encapsulatingPlaceholder.Nodes.Add(current);
                current.ParentPlaceholder = encapsulatingPlaceholder;
                encapsulatingPlaceholder.EncapsulateAllPartsOfNumberWithDigitsLeftOfIndex(currentIndex, siblingNodes);
                k.MoveRight();
            }
            else
            {
                encapsulatingPlaceholder.Nodes.Add(current);
                current.ParentPlaceholder = encapsulatingPlaceholder;
                k.MoveRight();
            }
        }
        else
        {
            k.Insert(newNode);
        }
    }

    public enum Options
    {
        None,
        DeleteOuterRoundBracketsIfAny,
    }
}
