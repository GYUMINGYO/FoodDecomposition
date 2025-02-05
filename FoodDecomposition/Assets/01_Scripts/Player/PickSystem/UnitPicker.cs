using GM.Entities;
using GM.GameEventSystem;

namespace GM.Players.Pickers
{
    public class UnitPicker : Picker
    {
        private Unit _pickedUnit;

        public void NotPickUnit()
        {
            _pickedUnit = null;
        }

        protected override void PickEntity()
        {
            if (_isRay == false)
            {
                UnitUIEvents.UnitDescriptionUIEvent.isActive = false;
                _uiDescriptionEventChannel.RaiseEvent(UnitUIEvents.UnitDescriptionUIEvent);
                _pickedUnit = null;

                return;
            }

            if (_hit.transform.TryGetComponent(out Unit unit))
            {
                if (_pickedUnit == unit) return;

                UnitUIEvents.UnitDescriptionUIEvent.type = DescriptionUIType.Unit;
                UnitUIEvents.UnitDescriptionUIEvent.unit = unit;
                UnitUIEvents.UnitDescriptionUIEvent.isActive = true;
                _uiDescriptionEventChannel.RaiseEvent(UnitUIEvents.UnitDescriptionUIEvent);

                _pickedUnit = unit;
            }
        }
    }
}
