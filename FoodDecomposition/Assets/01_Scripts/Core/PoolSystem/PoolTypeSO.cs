using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "SO/Pool/Type")]
public class PoolTypeSO : ScriptableObject
{
    public string typeName;
    public GameObject prefab;
    //public AssetReference assetRef;
    public int initCount;
}