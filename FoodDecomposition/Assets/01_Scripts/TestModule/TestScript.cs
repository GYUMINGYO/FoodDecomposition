using GM.Players;
using UnityEngine;

namespace GM.Test
{
    public class TestScript : MonoBehaviour
    {
        [SerializeField] private Player _player;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _player.Input.ChangeInputState(InputType.Player);
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                _player.Input.ChangeInputState(InputType.MapEdit);
            }
        }
    }
}