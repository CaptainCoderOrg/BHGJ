using CaptainCoder.Core;
namespace CaptainCoder.BloodyTetris;

public class Piece
{
    public static Func<Piece> IPiece => new PieceBuilder(BlockColor.Cyan)
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
            }).Build;

    public static Func<Piece> OPiece => new PieceBuilder(BlockColor.Yellow)
        .AddState(new string[]
        {
            "**",
            "**"
        })
        .Build;
    public static Func<Piece> JPiece => new PieceBuilder(BlockColor.Blue)
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
        .Build;
    public static Func<Piece> LPiece => new PieceBuilder(BlockColor.Orange)
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
        .Build;
    public static Func<Piece> TPiece => new PieceBuilder(BlockColor.Purple)
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
        .Build;
    public static Func<Piece> SPiece => new PieceBuilder(BlockColor.Green)
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
        .Build;
    public static Func<Piece> ZPiece => new PieceBuilder(BlockColor.Red)
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
        .Build;
    public static Func<Piece>[] All() => new[] { OPiece, JPiece, LPiece, IPiece, TPiece, SPiece, ZPiece };

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
    public bool IsBloody { get; private set; }

    public void Bleed()
    {
        IsBloody = true;
        foreach(var state in _states)
        {
            state.ToTuples().ToList().ForEach(pair => pair.Item2.Bleed());
        }
        
    }

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

        public Piece Build() 
        {
            List<Dictionary<Position, Block>> states = new();
            foreach (Dictionary<Position, Block> toClone in _states)
            {
                Dictionary<Position, Block> clone = new();
                foreach((Position p, Block b) in toClone)
                {
                    clone[p] = new Block(b.Color);
                }
                states.Add(clone);
            }
            return new Piece(states);
        }

    }
}

