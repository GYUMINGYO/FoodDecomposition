using RPG.StatSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Test
{
    public class StatTester : MonoBehaviour
    {
        [SerializeField] private StatSO _testStat;
        [SerializeField] private float _testValue = 3f;

        private void Update()
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                
            }
            
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                
            }
        }
    }
}
