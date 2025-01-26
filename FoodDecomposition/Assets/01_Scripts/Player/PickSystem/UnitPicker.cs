using GM.Entities;
using GM.EventSystem;
using UnityEngine;

namespace GM.Players.Pickers
{
    public class UnitPicker : Picker
    {
        [SerializeField] private GameEventChannelSO _unitPickUIEventChannel;

        private bool _isPick;
        private Unit _pickedUnit;

        protected override void PickEntity()
        {
            if (_isRay == false)
            {
                UnitUIEvents.UnitDescriptionUIEvent.isActive = false;
                _unitPickUIEventChannel.RaiseEvent(UnitUIEvents.UnitDescriptionUIEvent);
                _pickedUnit = null;
                _isPick = false;

                return;
            }

            if (_hit.transform.TryGetComponent(out Unit unit))
            {
                if (_pickedUnit == unit) return;

                UnitUIEvents.UnitDescriptionUIEvent.type = DescriptionUIType.Unit;
                UnitUIEvents.UnitDescriptionUIEvent.unit = unit;
                UnitUIEvents.UnitDescriptionUIEvent.isActive = true;
                _unitPickUIEventChannel.RaiseEvent(UnitUIEvents.UnitDescriptionUIEvent);

                _pickedUnit = unit;
                _isPick = true;
            }
        }
    }
}
