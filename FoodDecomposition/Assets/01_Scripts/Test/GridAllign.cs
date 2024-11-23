using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class GridAllign : MonoBehaviour
{
    [SerializeField] private Transform FloorContainer;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector3Int gridSize;
    [SerializeField] private Vector3 spacing;

    [SerializeField] private List<GameObject> instanceList;


    [ContextMenu("Generate")]
    public void Generate()
    {
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int z = 0; z < gridSize.z; z++)
            { 
                for (int x = 0; x < gridSize.x; x++)
                {
                    var obj = Instantiate(prefab, FloorContainer);
                    obj.transform.localPosition = prefab.transform.position + new Vector3(spacing.x*x, spacing.y * y, spacing.z * z);
                    instanceList.Add(obj);
                }
            }
        }
    }

    [ContextMenu("ClearList")]
    public void ClearList()
    {
        foreach(GameObject obj in instanceList)
            DestroyImmediate(obj);
        instanceList.Clear();
    }
}
