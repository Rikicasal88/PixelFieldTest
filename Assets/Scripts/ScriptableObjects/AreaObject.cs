using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AreaObject")]
public class AreaObject : ScriptableObject
{
    public AreaObjectType AreaObjectT;
    public GameObject Prefab;

    public AreaObject(AreaObjectType areaObjectT, GameObject prefab)
    {
        AreaObjectT = areaObjectT;
        Prefab = prefab;
    }
}

public enum AreaObjectType
{
    Box = 0,
    Candles = 1,
    Coil = 2,
    Pallet = 3,
    Bag = 4,


}
