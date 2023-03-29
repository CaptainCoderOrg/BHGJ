namespace CaptainCoder.BloodyTetris;

public class PieceGenerator : BagGenerator<Func<Piece>>
{    public PieceGenerator() : base(IRandom.Shared, Piece.All(), 7) { }
}