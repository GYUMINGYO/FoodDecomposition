using UnityEngine;
using GM.Entities;
using GM.Maps;

namespace GM.Players.Pickers
{
    public class MapPicker : Picker
    {
        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _player.Input.OnMapClickEvent += HandlePick;
        }

        void OnDestroy()
        {
            _player.Input.OnMapClickEvent -= HandlePick;
        }

        protected override void PickEntity()
        {
            if (_isRay == false) return;

            if (_hit.transform.TryGetComponent(out MapObject mapObject))
            {
                mapObject.OutLine();
            }
        }
    }
}
