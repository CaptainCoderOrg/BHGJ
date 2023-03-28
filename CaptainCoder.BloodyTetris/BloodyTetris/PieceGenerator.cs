namespace CaptainCoder.BloodyTetris;

public class PieceGenerator : BagGenerator<Piece>
{    public PieceGenerator() : base(IRandom.Shared, Piece.All(), 7) { }
}