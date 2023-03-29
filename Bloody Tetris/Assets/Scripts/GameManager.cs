using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainCoder.Core;
using CaptainCoder.BloodyTetris;


public class GameManager : MonoBehaviour
{
    public GameState GameState { get; private set; }= new();
    [SerializeField]
    private BoardRenderer _board;
    [SerializeField]
    private float _tickDelay = 1f;

    private float TickDelay
    {
        get => _tickDelay;
        set
        {
            _tickDelay = value;
            _delay = new WaitForSeconds(_tickDelay);
        }
    }

    private WaitForSeconds _delay;
    private Coroutine _ticker;

    void Awake()
    {
        Debug.Assert(_board != null);
        TickDelay = _tickDelay;
    }

    // Start is called before the first frame update
    void Start()
    {
        _board.RenderBoard(GameState);
        _ticker = StartCoroutine(Ticker());
    }


    private IEnumerator Ticker()
    {
        while (true)
        {
            yield return _delay;
            GameState.Tick();
            _board.RenderBoard(GameState);
        }
    }

    public void ResetTicker()
    {
        StopCoroutine(_ticker);
        _ticker = StartCoroutine(Ticker());
    }

    public void Redraw() => _board.RenderBoard(GameState);

}
