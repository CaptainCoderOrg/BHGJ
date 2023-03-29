using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainCoder.Core;
using CaptainCoder.BloodyTetris;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(GameManager))]
public class InputControls : MonoBehaviour
{
    private Coroutine _handleInput;
    private GameManager _manager;
    private GameState GameState => _manager.GameState;
    [SerializeField]
    private float _repeatActionStartDelay = 0.25f;
    [SerializeField]
    private float _actionRepeatDelay = 0.1f;
    private Inputs _controls;

    void Awake()
    {
        _manager = GetComponent<GameManager>();
        _controls = new Inputs();
    }

    void OnEnable()
    {
        _controls.TetrisControls.Enable();
        _controls.TetrisControls.Move.started += HandleMove;
        _controls.TetrisControls.Move.canceled += StopInput;
        _controls.TetrisControls.Drop.started += HandleDrop;
        _controls.TetrisControls.RotateClockwise.started += HandleRotateClockwise;
    }

    private void HandleRotateClockwise(CallbackContext ctx)
    {
        Debug.Log("Rotate?");
        if (GameState.TryRotateClockwise())
        {
            _manager.Redraw();
        }
    }

    private void HandleDrop(CallbackContext ctx)
    {
        while (GameState.TryMove((1, 0))) ;
        GameState.Tick();
        _manager.ResetTicker();
        _manager.Redraw();
    }


    private void HandleMove(CallbackContext ctx)
    {
        Vector2 rawInput = ctx.ReadValue<Vector2>();
        Position input = new(-(int)rawInput.y, (int)rawInput.x);
        if (_handleInput != null) { StopCoroutine(_handleInput); }
        _handleInput = StartCoroutine(Move(input));
    }

    private void StopInput(CallbackContext ctx)
    {
        if (_handleInput != null) { StopCoroutine(_handleInput); }
    }
    private IEnumerator Move(Position position) => RepeatAction(() =>
    {
        if (!GameState.TryMove(position)) { return false; }
        if (position.Row != 0)
        {
            _manager.ResetTicker();
        }
        return true;
    });

    private IEnumerator RepeatAction(System.Func<bool> action)
    {
        if (action.Invoke())
        {
            _manager.Redraw();
        }
        yield return new WaitForSeconds(_repeatActionStartDelay);
        while (true)
        {
            if (action.Invoke())
            {
                _manager.Redraw();
            }
            yield return new WaitForSeconds(_actionRepeatDelay);
        }
    }
}
