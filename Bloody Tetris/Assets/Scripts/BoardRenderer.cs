using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainCoder.Core;
using CaptainCoder.BloodyTetris;
using System.Linq;

public class BoardRenderer : MonoBehaviour
{

    [field: SerializeField]
    private Cell _cellTemplate;
    private Dictionary<Position, Cell> _cells = new ();

    const int ROWS = 20;
    const int COLS = 10;
    

    private void Awake()
    {
        foreach (Transform t in this.transform)
        {
            Destroy(t.gameObject);
        }

        for (int r = 0; r < ROWS; r++)
        {
            for (int c = 0; c < COLS; c++)
            {
                Cell cell = Instantiate(_cellTemplate, this.transform);
                cell.Clear();
                cell.transform.localPosition = new Vector3(c, -r, 0);
                _cells[(r,c)] = cell;
            }
        }
    }

    public void RenderBoard(GameState gameState)
    {
        Dictionary<Position, Block> blocks = gameState.Blocks.ToDictionary();
        foreach ((Position pos, Cell cell) in _cells)
        {
            if(!blocks.TryGetValue(pos, out Block block))
            {
                cell.Clear();
            }
            else
            {
                cell.Block = block;
            }
        }
    }

}
