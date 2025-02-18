using System;

[Serializable]
public enum MenuType
{
    staff,
    fablic,
    client,
    recipe,
    setting
}

namespace GM
{
    public static class MenuUIEvents
    {
        public static readonly OpneMenuEvent OpneMenuEvent = new();
    }

    public class OpneMenuEvent : GameEvent
    {
        public MenuType type;
    }
}
