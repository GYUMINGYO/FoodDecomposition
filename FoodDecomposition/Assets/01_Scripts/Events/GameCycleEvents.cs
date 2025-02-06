namespace GM.GameEventSystem
{
    public static class GameCycleEvents
    {
        public static readonly RestourantStartEvent RestourantStartEvent = new();
    }

    public class RestourantStartEvent : GameEvent
    {
        public bool open;
    }
}
