using CaptainCoder.Core;
namespace CaptainCoder.BloodyTetris;

public class Piece
{
    public static Piece IPiece() => new PieceBuilder(BlockColor.Cyan)
        .AddState(new string[]
            {
                " *  ",
                " *  ",
                " *  ",
                " *  ",
            })
        .AddState(new string[]
            {
                "    ",
                "****",
                "    ",
                "    ",
            })
        .AddState(new string[]
            {
                "  * ",
                "  * ",
                "  * ",
                "  * ",
            })
        .AddState(new string[]
            {
                "    ",
                "    ",
                "****",
                "    ",
            })
        .Build();

    public static Piece OPiece() => new PieceBuilder(BlockColor.Yellow)
        .AddState(new string[]
        {
            "**",
            "**"
        })
        .Build();
    public static Piece JPiece() => new PieceBuilder(BlockColor.Blue)
        .AddState(new string[]
        {
            "*  ",
            "***",
            "   "
        })
        .AddState(new string[]
        {
            " **",
            " * ",
            " * "
        })
        .AddState(new string[]
        {
            "   ",
            "***",
            "  *"
        })
        .AddState(new string[]
        {
            " * ",
            " * ",
            "** "
        })
        .Build();
    public static Piece LPiece() => new PieceBuilder(BlockColor.Orange)
        .AddState(new string[]
        {
            "  *",
            "***",
            "   "
        })
        .AddState(new string[]
        {
            " * ",
            " * ",
            " **"
        })
        .AddState(new string[]
        {
            "   ",
            "***",
            "*  "
        })
        .AddState(new string[]
        {
            "** ",
            " * ",
            " * "
        })
        .Build();
    public static Piece TPiece() => new PieceBuilder(BlockColor.Purple)
        .AddState(new string[]
        {
            " * ",
            "***",
            "   "
        })
        .AddState(new string[]
        {
            " * ",
            " **",
            " * "
        })
        .AddState(new string[]
        {
            "   ",
            "***",
            " * "
        })
        .AddState(new string[]
        {
            " * ",
            "** ",
            " * "
        })
        .Build();
    public static Piece SPiece() => new PieceBuilder(BlockColor.Green)
        .AddState(new string[]
        {
            " **",
            "** ",
            "   "
        })
        .AddState(new string[]
        {
            " * ",
            " **",
            "  *"
        })
        .AddState(new string[]
        {
            "   ",
            " **",
            "** "
        })
        .AddState(new string[]
        {
            "*  ",
            "** ",
            " * "
        })
        .Build();
    public static Piece ZPiece() => new PieceBuilder(BlockColor.Red)
        .AddState(new string[]
        {
            "** ",
            " **",
            "   "
        })
        .AddState(new string[]
        {
            "  *",
            " **",
            " * "
        })
        .AddState(new string[]
        {
            "   ",
            "** ",
            " **"
        })
        .AddState(new string[]
        {
            " *  ",
            "** ",
            "*  "
        })
        .Build();
    public static Piece[] All() => new[] { OPiece(), JPiece(), LPiece(), IPiece(), TPiece(), SPiece(), ZPiece() };

    private readonly List<Dictionary<Position, Block>> _states;
    private int _currentState;
    private int CurrentState
    {
        get => _currentState;
        set => _currentState = ((value % _states.Count) + _states.Count) % _states.Count;
    }

    private Piece(List<Dictionary<Position, Block>> states) => _states = states;
    public IEnumerable<(Position, Block)> Blocks => _states[CurrentState].ToTuples();
    public void RotateClockwise() => CurrentState++;
    public void RotateCounterClockwise() => CurrentState--;
    public int Rows => Blocks.Select(pair => pair.Item1.Row).ToHashSet().Count();
    public int Columns => Blocks.Select(pair => pair.Item1.Col).ToHashSet().Count();

    internal class PieceBuilder
    {
        private List<Dictionary<Position, Block>> _states = new();
        private BlockColor _color;
        public PieceBuilder(BlockColor color) => _color = color;

        public PieceBuilder AddState(string[] nextState)
        {

            Dictionary<Position, Block> blocks = new();
            for (int row = 0; row < nextState.Length; row++)
            {
                for (int col = 0; col < nextState[row].Length; col++)
                {
                    if (nextState[row][col] != '*') { continue; }
                    blocks[new Position(row, col)] = new Block(_color);

                }
            }
            _states.Add(blocks);
            return this;
        }

        public Piece Build() => new Piece(_states);
    }
}

