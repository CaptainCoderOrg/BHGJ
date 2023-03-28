using Moq;
using CaptainCoder.Core;
namespace CaptainCoder.BloodyTetris.Tests;

public class GameStateTest
{
    [Fact]
    public void TestTickAndMove()
    {
        Piece ipiece = Piece.IPiece();
        Mock<PieceGenerator> moqGen = new Mock<PieceGenerator>();
        moqGen.Setup(p => p.Next()).Returns(ipiece);
        // PieceGenerator moqGen 
        GameState state = new GameState(moqGen.Object);

        Assert.Equal(ipiece, state.Falling);
        Dictionary<Position, Block> blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(0, 6), blocks.Keys);
        Assert.Contains(new Position(1, 6), blocks.Keys);
        Assert.Contains(new Position(2, 6), blocks.Keys);
        Assert.Contains(new Position(3, 6), blocks.Keys);

        Assert.True(state.Tick());
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 6), blocks.Keys);
        Assert.Contains(new Position(2, 6), blocks.Keys);
        Assert.Contains(new Position(3, 6), blocks.Keys);
        Assert.Contains(new Position(4, 6), blocks.Keys);

        Assert.True(state.TryMove((0,1)));
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 7), blocks.Keys);
        Assert.Contains(new Position(2, 7), blocks.Keys);
        Assert.Contains(new Position(3, 7), blocks.Keys);
        Assert.Contains(new Position(4, 7), blocks.Keys);

        Assert.True(state.TryMove((0,1)));
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 8), blocks.Keys);
        Assert.Contains(new Position(2, 8), blocks.Keys);
        Assert.Contains(new Position(3, 8), blocks.Keys);
        Assert.Contains(new Position(4, 8), blocks.Keys);

        Assert.True(state.TryMove((0,1)));
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 9), blocks.Keys);
        Assert.Contains(new Position(2, 9), blocks.Keys);
        Assert.Contains(new Position(3, 9), blocks.Keys);
        Assert.Contains(new Position(4, 9), blocks.Keys);

        Assert.False(state.TryMove((0,1)));
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 9), blocks.Keys);
        Assert.Contains(new Position(2, 9), blocks.Keys);
        Assert.Contains(new Position(3, 9), blocks.Keys);
        Assert.Contains(new Position(4, 9), blocks.Keys);

        Assert.True(state.TryMove((0,-1)));
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 8), blocks.Keys);
        Assert.Contains(new Position(2, 8), blocks.Keys);
        Assert.Contains(new Position(3, 8), blocks.Keys);
        Assert.Contains(new Position(4, 8), blocks.Keys);

        Assert.True(state.TryMove((0,-1)));
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 7), blocks.Keys);
        Assert.Contains(new Position(2, 7), blocks.Keys);
        Assert.Contains(new Position(3, 7), blocks.Keys);
        Assert.Contains(new Position(4, 7), blocks.Keys);

        Assert.True(state.TryMove((0,-7)));
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 0), blocks.Keys);
        Assert.Contains(new Position(2, 0), blocks.Keys);
        Assert.Contains(new Position(3, 0), blocks.Keys);
        Assert.Contains(new Position(4, 0), blocks.Keys);

        Assert.False(state.TryMove((0,-1)));
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 0), blocks.Keys);
        Assert.Contains(new Position(2, 0), blocks.Keys);
        Assert.Contains(new Position(3, 0), blocks.Keys);
        Assert.Contains(new Position(4, 0), blocks.Keys);

        // Should kick away from wall
        Assert.True(state.TryRotateClockwise());
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(2, 0), blocks.Keys);
        Assert.Contains(new Position(2, 1), blocks.Keys);
        Assert.Contains(new Position(2, 2), blocks.Keys);
        Assert.Contains(new Position(2, 3), blocks.Keys);

        Assert.True(state.TryRotateClockwise());
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 2), blocks.Keys);
        Assert.Contains(new Position(2, 2), blocks.Keys);
        Assert.Contains(new Position(3, 2), blocks.Keys);
        Assert.Contains(new Position(4, 2), blocks.Keys);

        Assert.True(state.TryRotateClockwise());
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(3, 0), blocks.Keys);
        Assert.Contains(new Position(3, 1), blocks.Keys);
        Assert.Contains(new Position(3, 2), blocks.Keys);
        Assert.Contains(new Position(3, 3), blocks.Keys);

        Assert.True(state.TryRotateClockwise());
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 1), blocks.Keys);
        Assert.Contains(new Position(2, 1), blocks.Keys);
        Assert.Contains(new Position(3, 1), blocks.Keys);
        Assert.Contains(new Position(4, 1), blocks.Keys);

        Assert.True(state.TryRotateCounterClockwise());
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(3, 0), blocks.Keys);
        Assert.Contains(new Position(3, 1), blocks.Keys);
        Assert.Contains(new Position(3, 2), blocks.Keys);
        Assert.Contains(new Position(3, 3), blocks.Keys);

        Assert.True(state.TryRotateCounterClockwise());
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 2), blocks.Keys);
        Assert.Contains(new Position(2, 2), blocks.Keys);
        Assert.Contains(new Position(3, 2), blocks.Keys);
        Assert.Contains(new Position(4, 2), blocks.Keys);

        Assert.True(state.TryRotateCounterClockwise());
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(2, 0), blocks.Keys);
        Assert.Contains(new Position(2, 1), blocks.Keys);
        Assert.Contains(new Position(2, 2), blocks.Keys);
        Assert.Contains(new Position(2, 3), blocks.Keys);

        Assert.True(state.TryRotateCounterClockwise());
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 1), blocks.Keys);
        Assert.Contains(new Position(2, 1), blocks.Keys);
        Assert.Contains(new Position(3, 1), blocks.Keys);
        Assert.Contains(new Position(4, 1), blocks.Keys);

        Assert.True(state.TryMove((0, 8)));
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(1, 9), blocks.Keys);
        Assert.Contains(new Position(2, 9), blocks.Keys);
        Assert.Contains(new Position(3, 9), blocks.Keys);
        Assert.Contains(new Position(4, 9), blocks.Keys);

        Assert.True(state.TryRotateCounterClockwise());
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(3, 6), blocks.Keys);
        Assert.Contains(new Position(3, 7), blocks.Keys);
        Assert.Contains(new Position(3, 8), blocks.Keys);
        Assert.Contains(new Position(3, 9), blocks.Keys);

        Assert.True(state.TryMove((1, 0)));
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(4, 6), blocks.Keys);
        Assert.Contains(new Position(4, 7), blocks.Keys);
        Assert.Contains(new Position(4, 8), blocks.Keys);
        Assert.Contains(new Position(4, 9), blocks.Keys);

        Assert.True(state.TryMove((15, 0)));
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(19, 6), blocks.Keys);
        Assert.Contains(new Position(19, 7), blocks.Keys);
        Assert.Contains(new Position(19, 8), blocks.Keys);
        Assert.Contains(new Position(19, 9), blocks.Keys);

        Assert.False(state.TryMove((1, 0)));
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(4, blocks.Count());
        Assert.Contains(new Position(19, 6), blocks.Keys);
        Assert.Contains(new Position(19, 7), blocks.Keys);
        Assert.Contains(new Position(19, 8), blocks.Keys);
        Assert.Contains(new Position(19, 9), blocks.Keys);

        Piece jpiece = Piece.JPiece();
        moqGen.Setup(p => p.Next()).Returns(jpiece);
        // piece is at the bottom of the board, it should be set
        state.Tick();
        Assert.Equal(jpiece, state.Falling);
        blocks = state.Blocks.ToDictionary();
        Assert.Equal(8, blocks.Count());
        
        Assert.Contains(new Position(19, 6), blocks.Keys);
        Assert.Contains(new Position(19, 7), blocks.Keys);
        Assert.Contains(new Position(19, 8), blocks.Keys);
        Assert.Contains(new Position(19, 9), blocks.Keys);

    }
}