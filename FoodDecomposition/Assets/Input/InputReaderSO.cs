using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReaderSO", menuName = "SO/InputReaderSO")]
public class InputReaderSO : ScriptableObject, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions _inputSystem_Actions;
    public Vector3 Movement => movement;

    private Vector3 movement;

    private void OnEnable()
    {
        if (_inputSystem_Actions == null)
        {
            _inputSystem_Actions = new InputSystem_Actions();
            _inputSystem_Actions.Player.SetCallbacks(this);
        }
        _inputSystem_Actions.Enable();
    }

    private void OnDisable()
    {
        _inputSystem_Actions.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector3>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnInteract(InputAction.CallbackContext context)
    {

    }
}
