using Xunit;

namespace MathKeyboardEngine.Tests;

public class MatrixNode_Tests
{
    [Fact]
    public void PMatrix_width2_height3_1r2d4d6()
    {
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 3));
        Expect.Latex(@"\begin{pmatrix}▦ & ⬚ \\ ⬚ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
        k.Insert(new DigitNode("1"));
        k.MoveRight();
        k.Insert(new DigitNode("2"));
        k.MoveDown();
        k.Insert(new DigitNode("4"));
        k.MoveDown();
        k.Insert(new DigitNode("6"));
        Expect.Latex(@"\begin{pmatrix}1 & 2 \\ ⬚ & 4 \\ ⬚ & 6▦\end{pmatrix}", k);
    }
}
