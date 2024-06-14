using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAreaScript : MonoBehaviour
{
    public Transform[] SpawningPoints;
    public int ObjectRandomness = 40;
    public GameObject[] walls; // 0 - Up 1 -Down 2 - Right 3- Left
    public GameObject[] doors;
    public List<MeshRenderer> Renderers;

    public virtual void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
    public void AddObjects()
    {
        GameObject obj;
        foreach (Transform item in SpawningPoints)
        {
            if (Random.Range(0, 100) > ObjectRandomness)
            {
                obj = AreaObjectSpawner.Instance.GetObjectOfType((AreaObjectType)Random.Range(0, 5));
                obj.transform.SetParent(item);
                obj.transform.localPosition = Vector3.zero;
            }
        }
    }
}
