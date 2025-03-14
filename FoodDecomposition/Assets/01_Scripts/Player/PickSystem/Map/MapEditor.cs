using GM.Maps;
using UnityEngine;

namespace GM.Players.Pickers.Maps
{
    public abstract class MapEditor : MonoBehaviour
    {
        protected MapPicker _mapPicker;
        protected Player _player;
        protected Map _map;

        public abstract void MapEdit();
        public abstract void EndEdit();

        public void Initialize(MapPicker mapPicker, Map map, Player player)
        {
            _mapPicker = mapPicker;
            _map = map;
            _player = player;
        }
    }
}
