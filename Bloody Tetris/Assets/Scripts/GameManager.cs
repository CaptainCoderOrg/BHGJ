using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainCoder.Core;
using CaptainCoder.BloodyTetris;
using System.Linq;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public Dictionary<Position, Enemy> Enemies = new();
    [field: SerializeField]
    private AudioSource _squish;
    [field: SerializeField]
    private AudioSource _music;
    [field: SerializeField]
    private Enemy _enemyTemplate;
    public GameState GameState { get; private set; } = new();
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

    private Piece _falling;
    private WaitForSeconds _delay;
    private Coroutine _ticker;
    private Coroutine _drainBlood;

    [field: SerializeField]
    private int _blood = 99;
    public int Blood
    {
        get => _blood;
        private set
        {
            _blood = Mathf.Max(0, value);
            OnBloodChanged.Invoke(_blood);
        }
    }

    [field: SerializeField]
    private int _lines = 0;
    public int Lines
    {
        get => _lines;
        private set
        {
            _lines = value;
            int level = 1 + (_lines / 10);
            if (level > Level)
            {
                Level = level;
            }
            OnLinesChange.Invoke(_lines);
        }
    }

    [field: SerializeField]
    private int _level = 1;
    public int Level
    {
        get => _level;
        private set
        {
            _level = value;
            TickDelay = 1.8f / _level;
            OnLevelChanged.Invoke(_level);
        }
    }

    public UnityEvent<int> OnLinesChange;
    public UnityEvent<int> OnBloodChanged;
    public UnityEvent<int> OnLevelChanged;


    void Awake()
    {
        Debug.Assert(_board != null);
    }

    // Start is called before the first frame update
    // void Start() => StartGame();

    private IEnumerator DrainBlood()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Blood--;
            if (Blood <= 0)
            {
                StartCoroutine(Drop());
            }
        }
    }

    public IEnumerator Drop()
    {
        StopTicker();
        while (GameState.TryMove((1, 0)))
        {
            DestroyEnemies();
            Redraw();
            yield return new WaitForSeconds(0.01f);
        }
        Tick();
        _ticker = StartCoroutine(Ticker());
        if (Blood <= 0)
        {
            StartCoroutine(Drop());
        }
    }
    private void StopTicker()
    {
        if (_ticker == null) { return; }
        StopCoroutine(_ticker);
    }

    private void DestroyEnemies()
    {
        List<Enemy> toDestroy = new();
        foreach (Position p in GameState.Blocks.Select(pair => pair.Item1))
        {
            if (Enemies.ContainsKey(p))
            {
                toDestroy.Add(Enemies[p]);
            }
        }
        foreach (Enemy e in toDestroy)
        {
            _falling.Bleed();
            e.KillEnemy();
        }
    }

    private bool Tick()
    {
        bool result = GameState.Tick(out IEnumerable<int> clearedLines, out IEnumerable<Block> clearedBlocks);
        Lines += clearedLines.Count();
        Blood += clearedBlocks.Where(b => b.IsBloody).Count();
        _board.RenderBoard(GameState);
        if (_falling != GameState.Falling)
        {
            _falling = GameState.Falling;
            SpawnEnemy();
        }
        DestroyEnemies();
        if (result == false)
        {
            _music.Stop();
        }
        return result;
    }


    private IEnumerator Ticker()
    {
        while (true)
        {
            yield return _delay;
            Tick();
        }
    }

    public void ResetTicker()
    {
        StopTicker();
        _ticker = StartCoroutine(Ticker());
    }

    public void Redraw() => _board.RenderBoard(GameState);
    public void Bleed()
    {
        GameState.Falling.Bleed();
        _board.RenderBoard(GameState);
    }

    public void SpawnEnemy()
    {
        HashSet<Position> InvalidPositions = GameState.Blocks.Select(pair => pair.Item1).ToHashSet();
        int iterations = 0;
        while (iterations < 1000)
        {
            iterations++;
            Position p = new(Random.Range(10, 20), Random.Range(0, 10));
            if (InvalidPositions.Contains(p)) { continue; }
            Enemy e = Instantiate<Enemy>(_enemyTemplate, _board.transform);
            e.Position = p;
            e.Manager = this;
            e.transform.localPosition = new Vector3(p.Col, -p.Row, -1);
            Enemies[p] = e;
            break;
        }
    }

    public void StartGame()
    {
        StopAllCoroutines();
        foreach (var obj in GameObject.FindObjectsOfType<Enemy>())
        {
            Destroy(obj.gameObject);
        }
        Enemies.Clear();
        Level = 1;
        Blood = 99;
        Lines = 0;
        GameState = new GameState();
        TickDelay = 1;
        _board.RenderBoard(GameState);
        _ticker = StartCoroutine(Ticker());
        _drainBlood = StartCoroutine(DrainBlood());
        _music.Play();
    }

    public void PlaySquish()
    {
        _squish.Play();
    }

}
