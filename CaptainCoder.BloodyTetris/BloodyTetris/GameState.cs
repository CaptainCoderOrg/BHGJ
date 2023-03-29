namespace CaptainCoder.BloodyTetris;

public class GameState
{
    private readonly PieceGenerator _pieceGenerator = new();
    private Piece _falling = null!;
    private Position _cursor;
    private readonly List<Piece> _queue = new();

    public GameState(PieceGenerator generator)
    {
        Board = new Board(20, 10);
        _pieceGenerator = generator;
        FillQueue();
        NextPiece();
    }

    public GameState() : this(new PieceGenerator()) {}

    public Board Board { get; private set; }
    public Piece Falling => _falling;
    public int QueueSize { get; private set; } = 4;
    public IEnumerable<(Position, Block)> Blocks
    {
        get
        {
            foreach((Position pos, Block block) in _falling.Blocks)
            {
                yield return (_cursor + pos, block);
            }
            foreach ((Position pos, Block block) in Board.Blocks)
            {
                yield return (pos, block);
            }
        }
    }

    public bool TryMove(Position offset)
    {
        if (!Board.CanPlacePiece(_cursor + offset, _falling)) { return false; }
        _cursor += offset;
        return true;
    }

    public bool TryRotateClockwise() => TryRotate(_falling.RotateClockwise, _falling.RotateCounterClockwise);
    public bool TryRotateCounterClockwise() => TryRotate(_falling.RotateCounterClockwise, _falling.RotateClockwise);

    private bool TryRotate(Action Rotate, Action UnRotate)
    {
        Rotate.Invoke();
        if (Board.CanPlacePiece(_cursor, _falling)) { return true; }
        if (TryMove(new Position(0, 1))) { return true; }
        if (TryMove(new Position(0, 2))) { return true; }
        if (TryMove(new Position(0, -1))) { return true; }
        if (TryMove(new Position(0, -2))) { return true; }
        UnRotate.Invoke();
        return false;
    }

    /// <summary>
    /// Advances the game state by moving the current falling piece down. If the piece cannot
    /// move down, it is set. Returns true if the game should continue and false if the game
    /// has been lost. The out argument clearedRows contains the index of any row that was removed.
    /// </summary>
    public bool Tick(out IEnumerable<int> clearedRows)
    {
        if (Board.CanPlacePiece(_cursor + (1, 0), _falling))
        {
            _cursor += (1, 0);
            clearedRows = Enumerable.Empty<int>();
            return true;
        }
        Board.SetPiece(_cursor, _falling);
        clearedRows = Board.ClearRows();
        return NextPiece();
    }

    public bool Tick() => Tick(out var _);

    public IEnumerable<int> ClearLines() => Board.ClearRows();

    private void FillQueue()
    {
        while (_queue.Count < QueueSize)
        {
            _queue.Add(_pieceGenerator.Next().Invoke());
        }
    }

    private bool NextPiece()
    {
        _falling = _pieceGenerator.Next().Invoke();
        _cursor = (0, Board.Columns / 2);
        FillQueue();
        return AttemptPlacePieceAtTop();
    }

    private bool AttemptPlacePieceAtTop()
    {
        int attempts = _falling.Rows - 1;
        while(attempts > 0)
        {
            if(Board.CanPlacePiece(_cursor, _falling)) { return true; }
            _cursor -= (1, 0);
            attempts--;
        }
        return false;
    }
}