namespace GM.GameEventSystem
{
    public static class GameCycleEvents
    {
        public static readonly RestourantCycleEvent RestourantCycleEvent = new();
        public static readonly RestourantClosingTimeEvent RestourantClosingTimeEvent = new();
        public static readonly ReadyToRestourant ReadyToRestourant = new();
    }

    public class RestourantCycleEvent : GameEvent
    {
        public bool open;
    }

    public class RestourantClosingTimeEvent : GameEvent { }
    public class ReadyToRestourant : GameEvent { }
}
