using DG.Tweening;
using UnityEngine;

public enum TakableType
{
    Hamber = 0,
    Cheese = 1000,
}

public class TakableObject : MonoBehaviour
{
    public TakableType Type => _type;

    [SerializeField] protected TakableType _type;
    [SerializeField] protected float _jumpPower;
    [SerializeField] protected float _jumpDuration;

    public void Movement(Vector3 target)
    {
        transform.DOJump(target, _jumpPower, 1, _jumpDuration).SetEase(Ease.Linear);
    }
}
