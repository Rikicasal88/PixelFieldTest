using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorScript : BaseAreaScript
{
    [SerializeField] private CellType cType;

    public override void UpdateRoom(bool[] status)
    {
        for (int i = cType == CellType.CorridorVertical ? 0 : 2; i < (cType == CellType.CorridorVertical ? 2 : 4); i++)
        {
            doors[i - (cType == CellType.CorridorVertical ? 0 : 2)].SetActive(status[i]);
            walls[i - (cType == CellType.CorridorVertical ? 0 : 2)].SetActive(!status[i]);
        }
    }
}
