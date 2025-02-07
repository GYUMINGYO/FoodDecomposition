namespace GM.GameEventSystem
{
    public static class GameCycleEvents
    {
        public static readonly RestourantCycleEvent RestourantCycleEvent = new();
        public static readonly RestourantClosingTimeEvent RestourantClosingTimeEvent = new();
    }

    public class RestourantCycleEvent : GameEvent
    {
        public bool open;
    }

    public class RestourantClosingTimeEvent : GameEvent
    {

    }
}
