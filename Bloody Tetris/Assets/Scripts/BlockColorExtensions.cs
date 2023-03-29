using UnityEngine;
using CaptainCoder.BloodyTetris;

public static class BlockColorExtensions
{
    public static Color ToUnityColor(this BlockColor color)
    {
        return color switch
        {
            BlockColor.Blue => Color.blue,
            BlockColor.Cyan => Color.cyan,
            BlockColor.Green => Color.green,
            BlockColor.Orange => new Color32(0xFF, 0x99, 0x00, 0xFF),
            BlockColor.Purple => new Color32(0xFF, 0x00, 0xFF, 0xFF),
            BlockColor.Red => Color.red,
            BlockColor.Yellow => Color.yellow,
            _ => throw new System.ComponentModel.InvalidEnumArgumentException(),
        };
    }
}