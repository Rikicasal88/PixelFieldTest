using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerCharacter player;
    [SerializeField] private PlayerCharacter otherPlayer;
    [SerializeField] private Transform player1Spawner;
    [SerializeField] private Transform player2Spawner;

    [SerializeField] private Camera ARCamera;

    private ViewType currentCam = ViewType.ARView;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnPlayer()
    {
        // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
        player = PhotonNetwork.Instantiate(playerPrefab.name, PhotonNetwork.IsMasterClient ? player1Spawner.position : player2Spawner.position, Quaternion.identity, 0).GetComponent<PlayerCharacter>();
    }

    public void MovePlayer(Vector2 dir)
    {
        player.Move(new Vector3(dir.x, 0, dir.y));
    }

    public void RotatePlayer(float angles)
    {
        player.Rotate(angles);
    }

    public void PlayerAttack()
    {
        player.Attack();
    }

    public void SwitchCamera()
    {
        switch (currentCam)
        {
            case ViewType.PlayerView:
                currentCam = ViewType.ARView;
                ARCamera.gameObject.SetActive(true);
                player.SetCamera(false);
                break;
            case ViewType.ARView:
                currentCam = ViewType.PlayerView;
                ARCamera.gameObject.SetActive(false);
                player.SetCamera(true);
                break;
            
        }
    }

    public void SetOtherPlayer(PlayerCharacter other)
    {
        otherPlayer = other;
        otherPlayer.transform.position = !PhotonNetwork.IsMasterClient ? player1Spawner.position : player2Spawner.position;
    }
}


public enum ViewType
{
    PlayerView = 0,
    ARView = 1
}
