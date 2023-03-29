using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainCoder.BloodyTetris;
using System.Linq;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Cell : MonoBehaviour
{
    public GameManager _manager;
    public GameObject BleedSprite;
    public static Color EmptyCell = new Color32(0xA0, 0xD9, 0xFF, 24);
    private SpriteRenderer _renderer;
    private BoxCollider2D _collider;
    // Start is called before the first frame update
    void Awake()
    {
        _manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Debug.Assert(_manager != null);
        _renderer = GetComponent<SpriteRenderer>();
        Debug.Assert(_renderer != null);
        _collider = GetComponent<BoxCollider2D>();
        Debug.Assert(_collider != null);
    }

    private Block _block;
    public Block Block
    {
        get => _block;
        set
        {
            _block = value;
            _renderer.color = _block.Color.ToUnityColor();
            _collider.enabled = true;
            BleedSprite.SetActive(_block.IsBloody);
        }
    }
    public void Clear()
    {
        _renderer.color = EmptyCell;
        _collider.enabled = false;
        BleedSprite.SetActive(false);
    }

    public void Bleed() => _manager.Bleed();
}
