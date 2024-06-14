using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerationManager : MonoBehaviour
{
    public static DungeonGenerationManager Instance;
    public Transform Dungeon;
    public Vector2Int size;
    public Vector2Int startPos;

    [SerializeField] private Transform center;
    [SerializeField] private GameObject room;
    [SerializeField] private GameObject corridorHorizontal;
    [SerializeField] private GameObject corridorVertical;
    [SerializeField] private int randomness = 60;
    [SerializeField] private Material[] dungeonMats;
    [SerializeField] private float refreshRate = 0.5f;

    private List<MeshRenderer> renderers = new List<MeshRenderer>();
    private Area[,] board;
    private List<Vector2Int> nextpossibleRooms = new List<Vector2Int>();
    private List<Vector2Int> rooms = new List<Vector2Int>();
    private void Awake()
    {
        Instance = this;
        board = new Area[size.x, size.y];
    }


    private void Start()
    {
        GenerateDungeon();
        InstantiateDungeon();
    }
    public bool test = false;
    public Vector2Int cell;
  
    private Area[,] GenerateDungeon()
    {
        board[startPos.x, startPos.y] = new Area(GetRoomStatus(startPos, CellType.Room), CellType.Room, startPos);

        GetSiblings(startPos, board[startPos.x, startPos.y].Status);
        while (rooms.Count > 0)
        {
            GetSiblings(rooms[0], board[rooms[0].x, rooms[0].y].Status);
            rooms.RemoveAt(0);
        }
        return board;
    }

    private void GetSiblings(Vector2Int pos, bool[] status)
    {
        int t = 0;
        Vector2Int temp;
        List<Vector2Int> nextRooms = GetNextRoom(pos, status);
        rooms.AddRange(nextRooms);
        for (int i = 0; i < nextRooms.Count; i++)
        {
            if (nextRooms[i].x == 0 || nextRooms[i].x == size.x - 1 || nextRooms[i].y == 0 || nextRooms[i].y == size.y - 1)
            {
                board[nextRooms[i].x, nextRooms[i].y] = new Area(GetRoomStatus(nextRooms[i], CellType.Room), CellType.Room, nextRooms[i]);
            }
            else
            {
                t = Random.Range(0, 2);
                switch (t)
                {
                    case 0:
                        board[nextRooms[i].x, nextRooms[i].y] = new Area(GetRoomStatus(nextRooms[i], CellType.Room), CellType.Room, nextRooms[i]);
                        break;
                    case 1:
                        temp = pos - nextRooms[i];
                        board[nextRooms[i].x, nextRooms[i].y] = new Area(GetRoomStatus(nextRooms[i], temp.x != 0 ? CellType.CorridorHorizontal : CellType.CorridorVertical), temp.x != 0 ? CellType.CorridorHorizontal : CellType.CorridorVertical, nextRooms[i]);
                        break;
                }
            }
        }
    }

    private bool[] GetRoomStatus(Vector2Int currentRoom, CellType cType)
    {
        bool[] status = new bool[4];
        //check North neighbor
        if (currentRoom.y - 1 >= 0 && cType != CellType.CorridorHorizontal)
        {
            if (board[currentRoom.x, currentRoom.y - 1] == null)
            {
                if (cType == CellType.CorridorVertical || Random.Range(0, 100) < randomness)
                {
                    status[0] = true;
                }
            }
            else if (board[currentRoom.x, currentRoom.y - 1].Used && board[currentRoom.x, currentRoom.y - 1].Status[1])
            {
                status[0] = true;
            }
        }

        //check South neighbor
        if (currentRoom.y + 1 < size.y && cType != CellType.CorridorHorizontal)
        {
            if (board[currentRoom.x, currentRoom.y + 1] == null)
            {
                if (cType == CellType.CorridorVertical || Random.Range(0, 100) < randomness)
                {
                    status[1] = true;
                }
            }
            else if (board[currentRoom.x, currentRoom.y + 1].Used && board[currentRoom.x, currentRoom.y + 1].Status[0])
            {
                status[1] = true;
            }
        }

        //check West neighbor
        if (currentRoom.x - 1 >= 0 && cType != CellType.CorridorVertical)
        {
            if (board[currentRoom.x - 1, currentRoom.y] == null)
            {
                if (cType == CellType.CorridorHorizontal || Random.Range(0, 100) < randomness)
                {
                    status[2] = true;
                }
            }
            else if (board[currentRoom.x - 1, currentRoom.y].Used && board[currentRoom.x - 1, currentRoom.y].Status[3])
            {
                status[2] = true;
            }
        }

        //check East neighbor
        if (currentRoom.x + 1 < size.x && cType != CellType.CorridorVertical)
        {
            if (board[currentRoom.x + 1, currentRoom.y] == null)
            {
                if (cType == CellType.CorridorHorizontal || Random.Range(0, 100) < randomness)
                {
                    status[3] = true;
                }
            }
            else if (board[currentRoom.x + 1, currentRoom.y].Used && board[currentRoom.x + 1, currentRoom.y].Status[2])
            {
                status[3] = true;
            }
        }

        if (cType == CellType.CorridorVertical)
        {
            if (board[currentRoom.x - 1, currentRoom.y] != null && board[currentRoom.x - 1, currentRoom.y].Used)
            {
                board[currentRoom.x - 1, currentRoom.y].Status[2] = false;
                board[currentRoom.x - 1, currentRoom.y].Status[3] = false;
            }
            if (board[currentRoom.x + 1, currentRoom.y] != null && board[currentRoom.x + 1, currentRoom.y].Used)
            {
                board[currentRoom.x + 1, currentRoom.y].Status[2] = false;
                board[currentRoom.x + 1, currentRoom.y].Status[3] = false;
            }
        }

        if (cType == CellType.CorridorHorizontal)
        {
            if (board[currentRoom.x, currentRoom.y - 1] != null && board[currentRoom.x, currentRoom.y - 1].Used)
            {
                board[currentRoom.x, currentRoom.y - 1].Status[0] = false;
                board[currentRoom.x, currentRoom.y - 1].Status[1] = false;
            }
            if (board[currentRoom.x, currentRoom.y + 1] != null && board[currentRoom.x, currentRoom.y + 1].Used)
            {
                board[currentRoom.x, currentRoom.y + 1].Status[0] = false;
                board[currentRoom.x, currentRoom.y + 1].Status[1] = false;
            }
        }

        return status;
    }

    private List<Vector2Int> GetNextRoom(Vector2Int currentRoom, bool[] status)
    {
        nextpossibleRooms.Clear();

        for (int i = 0; i < 4; i++)
        {
            if (status[i])
            {
                switch (i)
                {
                    case 0:
                        if (board[currentRoom.x, currentRoom.y - 1] == null)
                        {
                            nextpossibleRooms.Add(new Vector2Int(currentRoom.x, currentRoom.y - 1));
                        }
                        break;
                    case 1:
                        if (board[currentRoom.x, currentRoom.y + 1] == null)
                        {
                            nextpossibleRooms.Add(new Vector2Int(currentRoom.x, currentRoom.y + 1));
                        }
                        break;
                    case 2:
                        if (board[currentRoom.x - 1, currentRoom.y] == null)
                        {
                            nextpossibleRooms.Add(new Vector2Int(currentRoom.x - 1, currentRoom.y));
                        }
                        break;
                    case 3:
                        if (board[currentRoom.x + 1, currentRoom.y] == null)
                        {
                            nextpossibleRooms.Add(new Vector2Int(currentRoom.x + 1, currentRoom.y));
                        }
                        break;
                }
            }
        }
        return nextpossibleRooms;
    }

    private void InstantiateDungeon()
    {
        Area a;
        BaseAreaScript newRoom;
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                if (board[i, j] != null)
                {
                    a = board[i, j];
                    newRoom = Instantiate(a.CType == CellType.Room ? room : a.CType == CellType.CorridorHorizontal ? corridorHorizontal : corridorVertical, new Vector3((i - size.x/2) * 6 , 0, -(j - size.y / 2) * 6f), Quaternion.identity, center).GetComponent<BaseAreaScript>();
                    newRoom.UpdateRoom(a.Status);
                    newRoom.AddObjects();
                    renderers.AddRange(newRoom.Renderers);
                    newRoom.name += " " + i + "-" + j;
                    if (a.CType == CellType.CorridorVertical)
                    {
                        newRoom.transform.localEulerAngles = new Vector3(0, 90, 0);
                    }

                    if (startPos.x == i && startPos.y == j)
                    {
                        GameManager.Instance.player1Spawner = ((RoomScript)newRoom).player1Spawner;
                        GameManager.Instance.player2Spawner = ((RoomScript)newRoom).player2Spawner;
                    }
                }
            }
        }
        Dungeon.localScale = new Vector3(0.3f, 0.3f, 0.3f);
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

[System.Serializable]
public class Area
{
    public bool Used = false;
    public CellType CType;
    public Vector2Int Position;
    public bool[] Status;

    public Area(bool[] status, CellType cType, Vector2Int position)
    {
        Used = true;
        CType = cType;
        Position = position;
        Status = status;
    }
}


public enum CellType
{
    Room = 0,
    CorridorHorizontal = 1,
    CorridorVertical = 2,
}