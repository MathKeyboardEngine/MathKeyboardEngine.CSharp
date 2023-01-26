namespace MathKeyboardEngine;
public class MatrixNode : BranchingNode
{
    private readonly string _matrixType;
    private readonly List<List<Placeholder>> _grid = new();
    private readonly int _width;
    private readonly int _height;

    public MatrixNode(string matrixType, int width, int height)
        : base(Enumerable.Range(0, width * height).Select(x => new Placeholder()).ToList())
    {
        _matrixType = matrixType;
        _width = width;
        _height = height;
        for (var i = 0; i < _height; i++)
        {
            var row = new List<Placeholder>();
            _grid.Add(row);
            for (var j = 0; j < _width; j++)
            {
                row.Add(Placeholders[i * _width + j]);
            }
        }
    }

    protected override string GetLatexPart(KeyboardMemory k, LatexConfiguration latexConfiguration)
    {
        var latex = @"\begin{" + _matrixType + "}";
        latex += string.Join(@" \\ ", _grid.Select(row => string.Join(" & ", row.Select(p => p.GetLatex(k, latexConfiguration)))));
        latex += @"\end{" + _matrixType + "}";
        return latex;
    }

    public override Placeholder? GetMoveDownSuggestion(Placeholder fromPlaceholder)
    {
        (var rowIndex, var columnIndex) = GetPositionOf(fromPlaceholder);
        var nextRowIndex = rowIndex + 1;
        if (nextRowIndex < _height)
        {
            return Placeholders[nextRowIndex * _width + columnIndex];
        }
        else
        {
            return null;
        }
    }

    public override Placeholder? GetMoveUpSuggestion(Placeholder fromPlaceholder)
    {
        (var rowIndex, var columnIndex) = GetPositionOf(fromPlaceholder);
        var previousRowIndex = rowIndex - 1;
        if (previousRowIndex >= 0)
        {
            return Placeholders[previousRowIndex * _width + columnIndex];
        }
        else
        {
            return null;
        }
    }

    private (int, int) GetPositionOf(Placeholder placeholder)
    {
        var index = Placeholders.IndexOf(placeholder);
        if (index == -1)
        {
            throw new Exception("The provided Placeholder is not part of this MatrixNode.");
        }
        var rowIndex = index / _width;
        var columnIndex = index - rowIndex * _width;
        return (rowIndex, columnIndex);
    }
}
