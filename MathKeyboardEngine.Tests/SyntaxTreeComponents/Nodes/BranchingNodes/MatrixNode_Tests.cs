using System;
using Xunit;

namespace MathKeyboardEngine.Tests;

public class MatrixNode_Tests
{
    [Fact]
    public void PMatrix_width2_height3()
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

    [Fact]
    public void Move_through_all_the_cells_of_a_MatrixNode_with_MoveLeft_and_MoveRight()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        k.Insert(new DigitNode("1"));
        k.MoveRight();
        k.MoveRight();
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        k.Insert(new DigitNode("4"));
        Expect.Latex(@"\begin{pmatrix}1 & ⬚ \\ 3 & 4▦\end{pmatrix}", k);
        // Act & Assert MoveLeft
        k.MoveLeft();
        Expect.Latex(@"\begin{pmatrix}1 & ⬚ \\ 3 & ▦4\end{pmatrix}", k);
        k.MoveLeft();
        Expect.Latex(@"\begin{pmatrix}1 & ⬚ \\ 3▦ & 4\end{pmatrix}", k);
        k.MoveLeft();
        Expect.Latex(@"\begin{pmatrix}1 & ⬚ \\ ▦3 & 4\end{pmatrix}", k);
        k.MoveLeft();
        Expect.Latex(@"\begin{pmatrix}1 & ▦ \\ 3 & 4\end{pmatrix}", k);
        k.MoveLeft();
        Expect.Latex(@"\begin{pmatrix}1▦ & ⬚ \\ 3 & 4\end{pmatrix}", k);
        k.MoveLeft();
        Expect.Latex(@"\begin{pmatrix}▦1 & ⬚ \\ 3 & 4\end{pmatrix}", k);
        k.MoveLeft();
        Expect.Latex(@"▦\begin{pmatrix}1 & ⬚ \\ 3 & 4\end{pmatrix}", k);
        // Act & Assert MoveRight
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}▦1 & ⬚ \\ 3 & 4\end{pmatrix}", k);
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}1▦ & ⬚ \\ 3 & 4\end{pmatrix}", k);
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}1 & ▦ \\ 3 & 4\end{pmatrix}", k);
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}1 & ⬚ \\ ▦3 & 4\end{pmatrix}", k);
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}1 & ⬚ \\ 3▦ & 4\end{pmatrix}", k);
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}1 & ⬚ \\ 3 & ▦4\end{pmatrix}", k);
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}1 & ⬚ \\ 3 & 4▦\end{pmatrix}", k);
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}1 & ⬚ \\ 3 & 4\end{pmatrix}▦", k);
    }

    [Fact]
    public void Move_out_of_an_empty_MatrixNode_to_the_previous_Node_and_back_in()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new DigitNode("2"));
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        Expect.Latex(@"2\begin{pmatrix}▦ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
        // Act & Assert
        k.MoveLeft();
        Expect.Latex(@"2▦\begin{pmatrix}⬚ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
        k.MoveRight();
        Expect.Latex(@"2\begin{pmatrix}▦ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
    }

    [Fact]
    public void Delete_content()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        k.Insert(new DigitNode("1"));
        k.MoveRight();
        k.Insert(new DigitNode("2"));
        k.MoveRight();
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        k.Insert(new DigitNode("4"));
        Expect.Latex(@"\begin{pmatrix}1 & 2 \\ 3 & 4▦\end{pmatrix}", k);
        // Act & Assert
        k.DeleteLeft();
        Expect.Latex(@"\begin{pmatrix}1 & 2 \\ 3 & ▦\end{pmatrix}", k);
        k.DeleteLeft();
        Expect.Latex(@"\begin{pmatrix}1 & 2 \\ ▦ & ⬚\end{pmatrix}", k);
        k.DeleteLeft();
        Expect.Latex(@"\begin{pmatrix}1 & ▦ \\ ⬚ & ⬚\end{pmatrix}", k);
        k.DeleteLeft();
        Expect.Latex(@"\begin{pmatrix}▦ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
        k.DeleteLeft();
        Expect.Latex("▦", k);
    }

    [Fact]
    public void MoveRight_MoveDown_MoveLeft_MoveUp()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        Expect.Latex(@"\begin{pmatrix}▦ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
        // Act & Assert
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}⬚ & ▦ \\ ⬚ & ⬚\end{pmatrix}", k);
        k.MoveDown();
        Expect.Latex(@"\begin{pmatrix}⬚ & ⬚ \\ ⬚ & ▦\end{pmatrix}", k);
        k.MoveLeft();
        Expect.Latex(@"\begin{pmatrix}⬚ & ⬚ \\ ▦ & ⬚\end{pmatrix}", k);
        k.MoveUp();
        Expect.Latex(@"\begin{pmatrix}▦ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
    }

    [Fact]
    public void Impossible_updown_requests_in_an_empty_MatrixNode_should_not_throw()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        Expect.Latex(@"\begin{pmatrix}▦ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
        // Act & Assert
        k.MoveUp();
        Expect.Latex(@"\begin{pmatrix}▦ & ⬚ \\ ⬚ & ⬚\end{pmatrix}", k);
        k.MoveDown();
        Expect.Latex(@"\begin{pmatrix}⬚ & ⬚ \\ ▦ & ⬚\end{pmatrix}", k);
        k.MoveDown();
        Expect.Latex(@"\begin{pmatrix}⬚ & ⬚ \\ ▦ & ⬚\end{pmatrix}", k);
        k.MoveRight();
        Expect.Latex(@"\begin{pmatrix}⬚ & ⬚ \\ ⬚ & ▦\end{pmatrix}", k);
        k.MoveDown();
        Expect.Latex(@"\begin{pmatrix}⬚ & ⬚ \\ ⬚ & ▦\end{pmatrix}", k);
        k.MoveUp();
        Expect.Latex(@"\begin{pmatrix}⬚ & ▦ \\ ⬚ & ⬚\end{pmatrix}", k);
        k.MoveUp();
        Expect.Latex(@"\begin{pmatrix}⬚ & ▦ \\ ⬚ & ⬚\end{pmatrix}", k);
    }

    [Fact]
    public void Impossible_updown_request_in_filled_MatrixNode_should_not_throw()
    {
        // Arrange
        var k = new KeyboardMemory();
        k.Insert(new MatrixNode("pmatrix", 2, 2));
        k.Insert(new DigitNode("1"));
        k.MoveRight();
        k.Insert(new DigitNode("2"));
        k.MoveRight();
        k.Insert(new DigitNode("3"));
        k.MoveRight();
        k.Insert(new DigitNode("4"));
        Expect.Latex(@"\begin{pmatrix}1 & 2 \\ 3 & 4▦\end{pmatrix}", k);
        // Act & Assert
        k.MoveDown();
        Expect.Latex(@"\begin{pmatrix}1 & 2 \\ 3 & 4▦\end{pmatrix}", k);
        k.MoveUp();
        Expect.Latex(@"\begin{pmatrix}1 & 2▦ \\ 3 & 4\end{pmatrix}", k);
        k.MoveUp();
        Expect.Latex(@"\begin{pmatrix}1 & 2▦ \\ 3 & 4\end{pmatrix}", k);
    }

    [Fact]
    public void GetMoveDownSuggestion_throws_if_it_is_called_for_a_Placeholder_that_is_not_part_of_the_MatrixNode()
    {
        var matrix = new MatrixNode("pmatrix", 2, 2);
        var placeholderThatIsNotPartOfTheMatrix = new Placeholder();
        var ex = Assert.Throws<Exception>(() => matrix.GetMoveDownSuggestion(placeholderThatIsNotPartOfTheMatrix));
        Assert.Equal("The provided Placeholder is not part of this MatrixNode.", ex.Message);
    }
}
