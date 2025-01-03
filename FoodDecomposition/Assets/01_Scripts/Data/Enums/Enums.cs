using Unity.Behavior;

public static class Enums
{
    [BlackboardEnum]
    public enum InteractableEntityType
    {
        // Action
        Order,
        Recipe,

        // Object
        Rest,
        Counter,
        FoodOut,

        // CookWare
        Refrigerator,
        Stove,
        CuttingBoard,
        Sink,
    }
}
