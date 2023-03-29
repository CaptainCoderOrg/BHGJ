using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
    public static Color EmptyCell = new Color32(0xA0, 0xD9, 0xFF, 24);
    private SpriteRenderer _renderer;
    // Start is called before the first frame update
    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        Debug.Assert(_renderer != null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color Color { get => _renderer.color; set => _renderer.color = value; }
    public void Clear() => _renderer.color = EmptyCell;
}
