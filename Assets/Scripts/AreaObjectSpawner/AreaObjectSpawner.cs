using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaObjectSpawner : MonoBehaviour
{
    public static AreaObjectSpawner Instance;

    public AreaObject[] Objects = new AreaObject[0];

    private List<MeshRenderer> renderers = new List<MeshRenderer>();
    private GameObject temp;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetObjectOfType(AreaObjectType t)
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            if (Objects[i].AreaObjectT == t)
            {
                temp = Instantiate(Objects[i].Prefab);
                renderers.Add(temp.GetComponent<MeshRenderer>());
                return temp;
            }
        }
        return null;
    }

    private void LateUpdate()
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(GameManager.Instance.GetCurrentCam());
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].enabled = GeometryUtility.TestPlanesAABB(planes, renderers[i].bounds);
        }
    }
}


