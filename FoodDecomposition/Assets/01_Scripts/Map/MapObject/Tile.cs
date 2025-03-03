using UnityEngine;

namespace GM.Maps
{
    public class Tile : MapObject
    {
        [SerializeField] private Material _black, _white;

        public void SetColor(Color color)
        {
            _meshRenderer.material = color == Color.black ? _black : _white;
        }
    }
}
