using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "SO/InputReaderSO")]
public class InputReaderSO : ScriptableObject, Controlls.IPlayerActions
{
    private Controlls _controlls;

    public event Action OnPickEvent;

    public Vector3 Movement => _movement;
    private Vector3 _movement;

    public Vector3 MousePosition => _mousePosition;
    private Vector3 _mousePosition;

    private void OnEnable()
    {
        if (_controlls == null)
        {
            _controlls = new Controlls();
            _controlls.Player.SetCallbacks(this);
        }
        _controlls.Enable();
    }

    private void OnDisable()
    {
        _controlls.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector3>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector3>();
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
}
