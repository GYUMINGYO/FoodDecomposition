using MKDir;
using UnityEngine;

public class SOManager : MonoSingleton<SOManager>
{
    [SerializeField] private InputReaderSO inputReaderSO;
    public InputReaderSO InputReaderSO => inputReaderSO;
}
