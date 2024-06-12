using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARDungeonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DungeonGenerationManager.Instance.Dungeon.SetParent(transform);
        DungeonGenerationManager.Instance.Dungeon.localPosition = new Vector3(0,0,0);
        DungeonGenerationManager.Instance.Dungeon.localEulerAngles = new Vector3(0, 0, 0);
        DungeonGenerationManager.Instance.Dungeon.gameObject.SetActive(true);
        GameManager.Instance.SpawnPlayer();
    }
}
