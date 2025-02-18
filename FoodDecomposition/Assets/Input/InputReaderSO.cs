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
public class InputReaderSO : ScriptableObject, Controlls.IPlayerActions, Controlls.IMapEditActions, Controlls.ICameraActions, Controlls.ICoreActions
{
    private Controlls _controlls;

    public event Action OnPickEvent;

    public event Action OnMapEditChangeEvent;

    #region MapEditActions

    public event Action OnMapClickEvent;
    public event Action<bool> OnMapDragEvent;

    #endregion

    public Vector2 Movement => _movement;
    private Vector2 _movement;

    public Vector2 MousePosition => _mousePosition;
    private Vector2 _mousePosition;

    private bool _isClicked;
    private bool _isMapEdit;

    private void OnEnable()
    {
        if (_controlls == null)
        {
            _controlls = new Controlls();
            _controlls.Player.SetCallbacks(this);
            _controlls.Core.SetCallbacks(this);
            _controlls.MapEdit.SetCallbacks(this);
            _controlls.Camera.SetCallbacks(this);
        }
        _controlls.Player.Enable();
        _controlls.Core.Enable();
        _controlls.Camera.Enable();
    }

    private void OnDisable()
    {
        _controlls.Disable();
    }

    public void ChangeInputState(InputType type)
    {
        // TODO : Camera는 예외로?
        _controlls.Disable();
        _controlls.Camera.Enable();
        _controlls.Core.Enable();

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

        if (_isClicked == true)
        {
            OnMapDragEvent?.Invoke(true);
        }
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
        if (context.started)
        {
            _isClicked = true;
        }
        else if (context.canceled)
        {
            _isClicked = false;
            OnMapDragEvent?.Invoke(false);
        }
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
    }

    public void OnMapEditChange(InputAction.CallbackContext context)
    {
        OnMapEditChangeEvent.Invoke();

        _isMapEdit = !_isMapEdit;

        if (_isMapEdit)
        {
            ChangeInputState(InputType.MapEdit);
        }
        else
        {
            ChangeInputState(InputType.Player);
        }
    }
}
