using UnityEngine;

namespace GM.Maps
{
    public class MapEditor : MonoBehaviour
    {
        [SerializeField] private Tile _tilePrefab;

        public int tileCount = 0;

        [ContextMenu("Tile Gen")]
        public void Tile()
        {
            Tile tile = Instantiate(_tilePrefab, transform.position, Quaternion.identity);
            tile.transform.parent = transform;
            tileCount++;
        }
    }
}
