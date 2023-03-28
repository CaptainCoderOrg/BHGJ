using CaptainCoder.Core;
namespace CaptainCoder.BloodyTetris;

public class Board
{
    private readonly Dictionary<Position, Block> _board = new();
    public Board(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
    }

    public int Rows { get; }
    public int Columns { get; }
    public IEnumerable<(Position Pos, Block Block)> Blocks => _board.ToTuples();

    public bool CanPlacePiece(Position topLeft, Piece piece) =>
        piece.Blocks
             .Select(pair => pair.Item1 + topLeft)
             .All(IsValidPosition);

    private bool IsValidPosition(Position p) => 
        p.Row >= 0 && p.Col >= 0 && p.Row < Rows && p.Col < Columns && !_board.ContainsKey(p);

    public IEnumerable<int> SetPiece(Position topLeft, Piece piece)
    {
        foreach ((Position p, Block b) in piece.Blocks)
        {
            _board[topLeft + p] = b;
        }
        return FindClearedLines();
    }
    /// <summary>
    /// Returns an enumerable containing each row that is currently full.
    /// </summary>
    public IEnumerable<int> FindClearedLines() => CheckClearLines(Rows - 1);
    /// <summary>
     /// Clears any row that is full and returns an enumerable containing the rows
     /// that were cleared.
    /// </summary>
    public IEnumerable<int> ClearRows()
    {
        int offset = 0;
        foreach(int i in FindClearedLines())
        {
            ClearRow(i + offset);
            yield return i;
            offset++;
        }
    }

    private IEnumerable<int> CheckClearLines(int row)
    {
        if (row < 0) { yield break; }
        bool isFull = _board.ToTuples().Where(((Position p, Block b) pair) => pair.p.Row == row).Count() == Columns;
        if (isFull) { yield return row; }
        foreach (int n in CheckClearLines(row - 1))
        {
            yield return n;
        }
    }

    private void ClearRow(int row)
    {
        if (row < 0) { return; };
        for (int column = 0; column < Columns; column++)
        {
            if (_board.TryGetValue(new Position(row - 1, column), out Block block))
            {
                _board[new Position(row, column)] = block;
            }
            else
            {
                _board.Remove(new Position(row, column));
            }
        }
        ClearRow(row - 1);
    }
}