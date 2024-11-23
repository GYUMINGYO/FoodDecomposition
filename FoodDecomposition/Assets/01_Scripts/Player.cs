using UnityEngine;

public class Player : MonoBehaviour
{
    private InputReaderSO input;
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        input = SOManager.Instance.InputReaderSO;
    }

    private void Update()
    {
        Debug.Log(input.Movement);
        controller.Move(input.Movement);
    }
}
