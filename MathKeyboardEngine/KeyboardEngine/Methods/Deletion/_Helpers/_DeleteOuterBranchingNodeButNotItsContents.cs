﻿namespace MathKeyboardEngine._Helpers;

public static class _DeleteOuterBranchingNodeButNotItsContents
{
    public static void DeleteOuterBranchingNodeButNotItsContents(this Placeholder nonEmptyPlaceholder)
    {
        var outerBranchingNode = nonEmptyPlaceholder.ParentNode!;
        var indexOfOuterBranchingNode = outerBranchingNode.ParentPlaceholder.Nodes.IndexOf(outerBranchingNode);
        outerBranchingNode.ParentPlaceholder.Nodes.RemoveAt(indexOfOuterBranchingNode);
        outerBranchingNode.ParentPlaceholder.Nodes.InsertRange(indexOfOuterBranchingNode, nonEmptyPlaceholder.Nodes);
        foreach (var node in nonEmptyPlaceholder.Nodes)
        {
            node.ParentPlaceholder = outerBranchingNode.ParentPlaceholder;
        }
    }
}
