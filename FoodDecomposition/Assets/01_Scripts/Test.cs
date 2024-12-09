using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField] private TakableObject _obj;

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            _obj.Movement(transform.position);
        }
    }
}
