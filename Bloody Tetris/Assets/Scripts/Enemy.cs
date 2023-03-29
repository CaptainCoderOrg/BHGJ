using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainCoder.Core;

public class Enemy : MonoBehaviour
{
    public Position Position;
    public GameManager Manager;
    private bool _spawned = false;
    private void Awake()
    {
        if (Manager == null)
        {
            Manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }
    }

    public void OnTriggerStay2D(Collider2D other) => Trigger(other);
    public void OnTriggerEnter2D(Collider2D other) => Trigger(other);
    public void OnTriggerExit2D(Collider2D other) => Trigger(other);

    private void Trigger(Collider2D other)
    {
        Cell cell = other?.GetComponent<Cell>();
        if (cell != null)
        {
            cell.Bleed();
            KillEnemy();
        }
    }

    public void KillEnemy()
    {
        Destroy(this.gameObject);
        
        if (_spawned == false)
        {
            // Manager.SpawnEnemy();
            Manager.PlaySquish();
            Manager.Enemies.Remove(Position);
            _spawned = true;
        }
    }
}
