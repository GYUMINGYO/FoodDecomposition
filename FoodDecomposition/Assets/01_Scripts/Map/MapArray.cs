namespace GM.Maps
{
    public class MapArray
    {
        public Tile tile;

        public MapObject mapObject => _mapObject;
        private MapObject _mapObject;

        public void SetMapObject(MapObject mapObject)
        {
            _mapObject = mapObject;
            _mapObject.OnDestoryObjectEvent += HandleObjectDestory;
        }

        private void HandleObjectDestory()
        {
            _mapObject = null;
        }
    }
}
