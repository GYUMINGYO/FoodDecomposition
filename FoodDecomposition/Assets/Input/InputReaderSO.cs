using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputType
{
    Player,
    UI,
    MapEdit,
}

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "SO/InputReaderSO")]
public class InputReaderSO : ScriptableObject, Controlls.IPlayerActions, Controlls.IMapEditActions
{
    private Controlls _controlls;

    public event Action OnPickEvent;

    #region MapEditActions

    public event Action OnMapClickEvent;
    public event Action OnMapDragEvent;

    #endregion

    public Vector2 Movement => _movement;
    private Vector2 _movement;

    public Vector2 MousePosition => _mousePosition;
    private Vector2 _mousePosition;

    private void OnEnable()
    {
        if (_controlls == null)
        {
            _controlls = new Controlls();
            _controlls.Player.SetCallbacks(this);
            _controlls.MapEdit.SetCallbacks(this);
        }
        _controlls.Player.Enable();
    }

    private void OnDisable()
    {
        _controlls.Disable();
    }

    public void ChangeInputState(InputType type)
    {
        _controlls.Disable();
        switch (type)
        {
            case InputType.Player:
                _controlls.Player.Enable();
                break;
            case InputType.UI:
                _controlls.UI.Enable();
                break;
            case InputType.MapEdit:
                _controlls.MapEdit.Enable();
                break;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {

    }

    public void OnPick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPickEvent?.Invoke();
        }
    }

    public void OnMapPoint(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }

    public void OnMapClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnMapClickEvent?.Invoke();
        }
    }

    public void OnMapDrag(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnMapDragEvent?.Invoke();
        }
    }
}
