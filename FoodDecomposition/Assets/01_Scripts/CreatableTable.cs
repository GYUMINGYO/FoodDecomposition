using UnityEngine;

public abstract class CreatableTable : MonoBehaviour
{
    protected Collector _collector;

    public abstract void StartAnimation();
    
    protected abstract void HandleEntered(Collider obj);
    
    protected virtual void Awake()
    {
        _collector = GetComponent<Collector>();
        _collector.OnEntered += HandleEntered;
    }

    protected virtual void Puted(IPutable putable)
    {
        
    }
    
    protected virtual void Taken(ITakable takable)
    {
        
    }


}
