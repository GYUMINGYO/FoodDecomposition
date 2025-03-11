using GM.Maps;
using GM.Players;
using GM.Players.Pickers;
using UnityEngine;

namespace GM.Test
{
    public class TestScript : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private MapPicker _mapPicker;
        [SerializeField] private ObjectInfoSO _objectInfo;
        [SerializeField] private ObjectInfoSO _stove;
        [SerializeField] private Map _map;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                _mapPicker.SetMapObject(_stove);
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                _map.SetTemporaryTile(_objectInfo, new Vector3Int(749, 0, 750), new Vector3Int(751, 751));
                _map.SetTileObject();
            }
        }
    }
}