using UnityEngine;

public enum TakableType
{
    
}

public class TakableObject : MonoBehaviour
{
    public TakableType Type => _type;
    [SerializeField] private TakableType _type;

    public void Movement(Vector3 start, Vector3 end) 
    {
        
    }
}
