using MathKeyboardEngine.__Helpers;

namespace MathKeyboardEngine;

public static class __InsertWithEncapsulateCurrent
{
    public static void InsertWithEncapsulateCurrent(this KeyboardMemory k, BranchingNode newNode, InsertWithEncapsulateCurrentOptions? options = null)
    {
        var encapsulatingPlaceholder = newNode.Placeholders[0];
        if (k.Current is TreeNode current)
        {
            var siblingNodes = current.ParentPlaceholder.Nodes;
            var currentIndex = siblingNodes.IndexOf(current);
            siblingNodes[currentIndex] = newNode;
            newNode.ParentPlaceholder = current.ParentPlaceholder;
            if (current is RoundBracketsNode b && options == InsertWithEncapsulateCurrentOptions.DeleteOuterRoundBracketsIfAny)
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
}

public enum InsertWithEncapsulateCurrentOptions
{
    None,
    DeleteOuterRoundBracketsIfAny,
}
