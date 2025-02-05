using GM.Entities;

namespace GM.GameEventSystem
{
    public enum DescriptionUIType
    {
        Unit,
        Table,
    }

    public static class UnitUIEvents
    {
        public static readonly UnitDescriptionUIEvent UnitDescriptionUIEvent = new();
    }

    public class UnitDescriptionUIEvent : GameEvent
    {
        public DescriptionUIType type;
        public Unit unit;
        public bool isActive;
    }
}