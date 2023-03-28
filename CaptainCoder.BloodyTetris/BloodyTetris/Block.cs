namespace CaptainCoder.BloodyTetris;

public class Block
{
    public Block(BlockColor color) => Color = color;
    public bool IsBloody { get; private set; }
    public BlockColor Color { get; }
    public void Bleed() => IsBloody = true;
}