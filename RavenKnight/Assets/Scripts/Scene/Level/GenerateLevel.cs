using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NavMeshPlus.Components;
using Project.GenerateLevel;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField] private List<TileBase> tilesFloor;
    [SerializeField] private List<TileBase> tilesWall;
    [SerializeField] private List<TileBase> tilesSidewall;
    [Space(10)]
    [SerializeField] private List<GameObject> boostersList;
    [Space(10)]
    [SerializeField] private List<GameObject> obstacleList;
    [SerializeField] private List<GameObject> obstacleNearWallList;
    [SerializeField, Range(0, 100)] private int fillObstaclePercent;
    [Space(10)]
    [SerializeField] private Tilemap tilemapFloor;
    [SerializeField] private Tilemap tilemapWall;
    [SerializeField] private Tilemap tilemapSidewallIn;
    [SerializeField] private Tilemap tilemapSidewallOut;
    [SerializeField] private Tilemap tilemapGate;
    [Space(10)]
    [SerializeField] private NavMeshSurface navSurface1;
    [SerializeField] private NavMeshSurface navSurface2;
    [Space(10)]
    [SerializeField] private Transform parentSpawners;
    [SerializeField] private GameObject playerSpawner;
    [SerializeField] private GenerateEnemySpawner enemySpawner;
    [SerializeField] private GameObject prefabLoadNextLevel;
    [Space(10)]
    [SerializeField, Min(1)] private int spacingRoom = 20;
    [SerializeField] private LevelDifficult[] levelsList;

    private int currLevel = 0;
    private List<Room> currRooms;


    private Transform obstacleFolder;
    private Transform tempFolder;

    void Start()
    {
        GlobalEvents.bossRoomClear += OnBossRoomClear;
        GlobalEvents.loadNextLevel += OnLoadNextLevel;

        var goObstacleFolder = GameObject.Find("Obstacles");
        if (!goObstacleFolder)
            obstacleFolder = new GameObject("Obstacles").transform;
        else
            obstacleFolder = goObstacleFolder.transform;

        var goTempFolder = GameObject.Find("Temp");
        if (!goTempFolder)
            tempFolder = new GameObject("Temp").transform;
        else
            tempFolder = goTempFolder.transform;

        currRooms = CreateLevel();



    }

    private void OnDestroy()
    {
        GlobalEvents.bossRoomClear -= OnBossRoomClear;
        GlobalEvents.loadNextLevel -= OnLoadNextLevel;
    }

    private void OnValidate()
    {
        if (levelsList.Length == 0) return;
        int index = 1;
        foreach (var level in levelsList)
        {
            level.name = $"Level - {index}";
            index++;
        }
    }

    public int GetRoomDifficult(int roomID)
    {
        int divider = 0;
        for (int i = 1; i <= levelsList[currLevel].lengthMaze; i++)
            divider += i;

        int result = (levelsList[currLevel].difficultScore / divider) * roomID;

        return result;
    }

    private List<Room> CreateLevel()
    {
        parentSpawners.gameObject.SetActive(false);

        List<Room> rooms = CreateMaze(levelsList[currLevel].lengthMaze);
        int currRoom = 0;
        foreach (Room room in rooms)
        {
            CreateRoom(room, currRoom);
            currRoom++;
        }
        BakeNavMesh();

        parentSpawners.gameObject.SetActive(true);
        return rooms;



        List<Room> CreateMaze(int lenghtMaze)
        {
            if (lenghtMaze < 1) lenghtMaze = 1;
            List<Room> rooms = new List<Room>();

            int minSizeRoom = spacingRoom / 4;
            int maxSizeRoom = (spacingRoom / 2) - 1;
            Vector2Int startPosition = new Vector2Int(0, 0);
            Room currRoom;
            AddStartRoom();
            AddEnemyRoom();
            AddAdditionalRoom();

            return rooms;

            // Local Methots
            void AddStartRoom()
            {
                currRoom = new Room();
                currRoom.position = startPosition;
                currRoom.size = minSizeRoom;
                currRoom.roomType = RoomTypes.PlayerRoom;
                rooms.Add(currRoom);
            }

            void AddEnemyRoom()
            {
                Room nextRoom;
                lenghtMaze++;
                int currLenghtMaze = 0;
                while (currLenghtMaze < lenghtMaze)
                {
                    nextRoom = new Room();
                    bool repeat = true;
                    bool allDirBlocked = false;

                    int direction = 0;
                    int j = 1;
                    int[] checkingDir = new int[4] { -1, -1, -1, -1 };
                    while (repeat)
                    {
                        direction = Random.Range(0, 4);
                        CheckDirection();
                        SetRoomPosition();
                        CheckLogic();

                        void CheckDirection()
                        {
                            foreach (int dir in checkingDir)
                            {
                                if (direction == dir) direction++;
                                if (direction >= 4) direction = 0;
                            }
                        }

                        void SetRoomPosition()
                        {
                            switch (direction)
                            {
                                case 0: // Up
                                    nextRoom.position = currRoom.position + Vector2Int.up;
                                    break;
                                case 1: // Right
                                    nextRoom.position = currRoom.position + Vector2Int.right;
                                    break;
                                case 2: // Down
                                    nextRoom.position = currRoom.position + Vector2Int.down;
                                    break;
                                case 3: // Left
                                    nextRoom.position = currRoom.position + Vector2Int.left;
                                    break;
                            }
                        }

                        void CheckLogic()
                        {
                            repeat = false;
                            foreach (Room room in rooms)
                            {
                                if (room.position == nextRoom.position)
                                {
                                    repeat = true;
                                    checkingDir[direction] = direction;
                                    break;
                                }
                            }

                            allDirBlocked = true;
                            foreach (int dir in checkingDir)
                            {
                                if (dir == -1)
                                {
                                    allDirBlocked = false;
                                    break;
                                }
                            }

                            if (allDirBlocked)
                            {
                                j++;
                                currRoom = rooms[rooms.Count - j];
                                for (int i = 0; i < checkingDir.Length; i++) checkingDir[i] = -1;
                            }
                        }
                    }

                    if (allDirBlocked)
                    {
                        AddBossRoom();
                        break;
                    }


                    currLenghtMaze++;
                    SetData();
                    currRoom = nextRoom;
                    if (currLenghtMaze >= lenghtMaze)
                        AddBossRoom();

                    void SetData()
                    {
                        nextRoom.roomType = RoomTypes.EnemyRoom;
                        nextRoom.size = Random.Range(minSizeRoom, maxSizeRoom);
                        switch (direction)
                        {
                            case 0: // Up
                                currRoom.corridorUp = true;
                                nextRoom.corridorDown = true;
                                break;
                            case 1: // Right
                                currRoom.corridorRight = true;
                                nextRoom.corridorLeft = true;
                                break;
                            case 2: // Down
                                currRoom.corridorDown = true;
                                nextRoom.corridorUp = true;
                                break;
                            case 3: // Left
                                currRoom.corridorLeft = true;
                                nextRoom.corridorRight = true;
                                break;
                        }
                        rooms.Add(nextRoom);
                    }

                    void AddBossRoom()
                    {
                        currRoom.roomType = RoomTypes.FinalRoom;
                        currRoom.size = maxSizeRoom;
                    }
                }
            }

            void AddAdditionalRoom()
            {
                for (int i = 0; i < levelsList[currLevel].additionalRoom; i++)
                {
                    int randomID = Random.Range(1, levelsList.Length - 2);
                    Room nextRoom = new Room();
                    bool repeat = true;
                    bool allDirBlocked = false;
                    int direction = 0;
                    int[] checkingDir = new int[4] { -1, -1, -1, -1 };
                    if (rooms[randomID].corridorUp) checkingDir[0] = 0;
                    if (rooms[randomID].corridorRight) checkingDir[1] = 1;
                    if (rooms[randomID].corridorDown) checkingDir[2] = 2;
                    if (rooms[randomID].corridorLeft) checkingDir[3] = 3;

                    while (repeat)
                    {
                        direction = Random.Range(0, 4);
                        CheckDirection();
                        SetRoomPosition();
                        CheckLogic();

                        void CheckDirection()
                        {
                            foreach (int dir in checkingDir)
                            {
                                if (direction == dir) direction++;
                                if (direction >= 4) direction = 0;
                            }
                        }

                        void SetRoomPosition()
                        {
                            switch (direction)
                            {
                                case 0: // Up
                                    nextRoom.position = rooms[randomID].position + Vector2Int.up;
                                    break;
                                case 1: // Right
                                    nextRoom.position = rooms[randomID].position + Vector2Int.right;
                                    break;
                                case 2: // Down
                                    nextRoom.position = rooms[randomID].position + Vector2Int.down;
                                    break;
                                case 3: // Left
                                    nextRoom.position = rooms[randomID].position + Vector2Int.left;
                                    break;
                            }
                        }

                        void CheckLogic()
                        {
                            repeat = false;
                            foreach (Room room in rooms)
                            {
                                if (room.position == nextRoom.position)
                                {
                                    repeat = true;
                                    checkingDir[direction] = direction;
                                    break;
                                }
                            }

                            allDirBlocked = true;
                            foreach (int dir in checkingDir)
                            {
                                if (dir == -1)
                                {
                                    allDirBlocked = false;
                                    break;
                                }
                            }

                            if (allDirBlocked)
                            {
                                randomID = Random.Range(1, levelsList.Length - 2);
                                for (int i = 0; i < checkingDir.Length; i++) checkingDir[i] = -1;
                                if (rooms[randomID].corridorUp) checkingDir[0] = 0;
                                if (rooms[randomID].corridorRight) checkingDir[1] = 1;
                                if (rooms[randomID].corridorDown) checkingDir[2] = 2;
                                if (rooms[randomID].corridorLeft) checkingDir[3] = 3;
                            }
                        }
                    }

                    SetData();

                    void SetData()
                    {
                        nextRoom.roomType = RoomTypes.AdditionalRoom;
                        nextRoom.size = minSizeRoom;
                        switch (direction)
                        {
                            case 0: // Up
                                rooms[randomID].corridorUp = true;
                                nextRoom.corridorDown = true;
                                break;
                            case 1: // Right
                                rooms[randomID].corridorRight = true;
                                nextRoom.corridorLeft = true;
                                break;
                            case 2: // Down
                                rooms[randomID].corridorDown = true;
                                nextRoom.corridorUp = true;
                                break;
                            case 3: // Left
                                rooms[randomID].corridorLeft = true;
                                nextRoom.corridorRight = true;
                                break;
                        }
                        rooms.Add(nextRoom);
                    }
                }
            }
        }

        void CreateRoom(Room room, int index)
        {
            Vector2Int offsetRoom = new Vector2Int(spacingRoom * room.position.x, spacingRoom * room.position.y);

            CreateFloor();
            CreateWall();
            CreateSideWall();
            DeleteSegmentWall();
            CreateCorridor();
            CreateGate();
            CreateSpawner();
            FillObstacle();

            // Local Methods
            void CreateFloor()
            {
                for (int y = room.size - 1; y > -room.size; y--)
                {
                    for (int x = -room.size + 1; x < room.size; x++)
                    {
                        TileBase tile = tilesFloor[Random.Range(0, tilesFloor.Count)];
                        tilemapFloor.SetTile(new Vector3Int(x + offsetRoom.x, y + offsetRoom.y, 0), tile);
                    }
                }
            }

            void CreateWall()
            {
                TileBase tile;

                for (int x = -room.size; x <= room.size; x++)
                {
                    tile = tilesWall[Random.Range(0, tilesWall.Count)];
                    tilemapWall.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + room.size, 0), tile);
                    tile = tilesWall[Random.Range(0, tilesWall.Count)];
                    tilemapWall.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y - room.size, 0), tile);
                }
                for (int y = -room.size; y <= room.size; y++)
                {
                    tile = tilesWall[Random.Range(0, tilesWall.Count)];
                    tilemapWall.SetTile(new Vector3Int(offsetRoom.x - room.size, offsetRoom.y + y, 0), tile);
                    tile = tilesWall[Random.Range(0, tilesWall.Count)];
                    tilemapWall.SetTile(new Vector3Int(offsetRoom.x + room.size, offsetRoom.y + y, 0), tile);
                }
            }

            void CreateSideWall()
            {
                TileBase tile;

                for (int x = -room.size; x <= room.size; x++)
                {
                    if (x != -room.size && x != room.size)
                    {
                        tile = tilesSidewall[Random.Range(0, tilesSidewall.Count)];
                        tilemapSidewallIn.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + room.size - 1, 0), tile);
                    }
                    tile = tilesSidewall[Random.Range(0, tilesSidewall.Count)];
                    tilemapSidewallOut.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y - room.size - 1, 0), tile);
                }
            }

            void DeleteSegmentWall()
            {
                if (room.corridorUp)
                {
                    int y = room.size;
                    for (int x = -1; x <= 1; x++)
                    {
                        tilemapWall.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y, 0), null);
                        tilemapSidewallIn.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y - 1, 0), null);
                    }
                }
                if (room.corridorDown)
                {
                    int y = -room.size;
                    for (int x = -1; x <= 1; x++)
                    {
                        tilemapWall.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y, 0), null);
                        tilemapSidewallOut.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y - 1, 0), null);
                    }
                }
                if (room.corridorRight)
                {
                    int x = room.size;
                    for (int y = -1; y <= 1; y++)
                        tilemapWall.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y, 0), null);
                }
                if (room.corridorLeft)
                {
                    int x = -room.size;
                    for (int y = -1; y <= 1; y++)
                        tilemapWall.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y, 0), null);
                }
            }

            void CreateCorridor()
            {
                TileBase tile;

                if (room.corridorUp)
                {
                    for (int y = room.size; y <= spacingRoom / 2; y++)
                    {
                        for (int x = -1; x <= 1; x++)
                        {
                            tile = tilesFloor[Random.Range(0, tilesFloor.Count)];
                            tilemapFloor.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y, 0), tile);
                        }
                        tile = tilesWall[Random.Range(0, tilesWall.Count)];
                        tilemapWall.SetTile(new Vector3Int(offsetRoom.x - 2, offsetRoom.y + y, 0), tile);
                        tile = tilesWall[Random.Range(0, tilesWall.Count)];
                        tilemapWall.SetTile(new Vector3Int(offsetRoom.x + 2, offsetRoom.y + y, 0), tile);
                    }
                }
                if (room.corridorDown)
                {
                    for (int y = -room.size; y >= -spacingRoom / 2; y--)
                    {
                        for (int x = -1; x <= 1; x++)
                        {
                            tile = tilesFloor[Random.Range(0, tilesFloor.Count)];
                            tilemapFloor.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y, 0), tile);
                        }
                        tile = tilesWall[Random.Range(0, tilesWall.Count)];
                        tilemapWall.SetTile(new Vector3Int(offsetRoom.x - 2, offsetRoom.y + y, 0), tile);
                        tile = tilesWall[Random.Range(0, tilesWall.Count)];
                        tilemapWall.SetTile(new Vector3Int(offsetRoom.x + 2, offsetRoom.y + y, 0), tile);
                    }
                }
                if (room.corridorRight)
                {
                    for (int x = room.size; x <= spacingRoom / 2; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            tile = tilesFloor[Random.Range(0, tilesFloor.Count)];
                            tilemapFloor.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y, 0), tile);
                        }
                        tile = tilesWall[Random.Range(0, tilesWall.Count)];
                        tilemapWall.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + 2, 0), tile);
                        tile = tilesSidewall[Random.Range(0, tilesSidewall.Count)];
                        tilemapSidewallIn.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + 1, 0), tile);
                        tile = tilesWall[Random.Range(0, tilesWall.Count)];
                        tilemapWall.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y - 2, 0), tile);
                        if (x != -room.size && x != room.size)
                        {
                            tile = tilesSidewall[Random.Range(0, tilesSidewall.Count)];
                            tilemapSidewallOut.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y - 3, 0), tile);
                        }
                    }
                }
                if (room.corridorLeft)
                {
                    for (int x = -room.size; x >= -spacingRoom / 2; x--)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            tile = tilesFloor[Random.Range(0, tilesFloor.Count)];
                            tilemapFloor.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y, 0), tile);
                        }
                        tile = tilesWall[Random.Range(0, tilesWall.Count)];
                        tilemapWall.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y - 2, 0), tile);
                        tile = tilesSidewall[Random.Range(0, tilesSidewall.Count)];
                        tilemapSidewallIn.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + 1, 0), tile);
                        tile = tilesWall[Random.Range(0, tilesWall.Count)];
                        tilemapWall.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + 2, 0), tile);
                        if (x != -room.size && x != room.size)
                        {
                            tile = tilesSidewall[Random.Range(0, tilesSidewall.Count)];
                            tilemapSidewallOut.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y - 3, 0), tile);
                        }
                    }
                }
            }

            void CreateGate()
            {
                TileBase tile;

                if (room.corridorUp)
                {
                    int y = room.size + 1;
                    for (int x = -1; x <= 1; x++)
                    {
                        tile = tilesWall[Random.Range(0, tilesWall.Count)];
                        tilemapGate.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y, 0), tile);
                    }
                }
                if (room.corridorDown)
                {
                    int y = -room.size - 1;
                    for (int x = -1; x <= 1; x++)
                    {
                        tile = tilesWall[Random.Range(0, tilesWall.Count)];
                        tilemapGate.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y, 0), tile);
                    }
                }
                if (room.corridorRight)
                {
                    int x = room.size + 1;
                    for (int y = -1; y <= 1; y++)
                    {
                        tile = tilesWall[Random.Range(0, tilesWall.Count)];
                        tilemapGate.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y, 0), tile);
                    }
                }
                if (room.corridorLeft)
                {
                    int x = -room.size - 1;
                    for (int y = -1; y <= 1; y++)
                    {
                        tile = tilesWall[Random.Range(0, tilesWall.Count)];
                        tilemapGate.SetTile(new Vector3Int(offsetRoom.x + x, offsetRoom.y + y, 0), tile);
                    }
                }
            }

            void CreateSpawner()
            {
                Vector3 pointV3 = new Vector3(offsetRoom.x, offsetRoom.y, 0);
                Vector3 offsetV3 = new Vector3(0.5f, 0.5f, 0);
                GameObject playerSpawner;
                GenerateEnemySpawner enemySpawner;
                GameObject boosterGO;
                switch (room.roomType)
                {
                    case RoomTypes.PlayerRoom:
                        playerSpawner = Instantiate(this.playerSpawner, parentSpawners);
                        playerSpawner.transform.position = pointV3 + offsetV3;
                        break;
                    case RoomTypes.EnemyRoom:
                        enemySpawner = Instantiate(this.enemySpawner, parentSpawners);
                        enemySpawner.transform.position = pointV3 + offsetV3;
                        enemySpawner.spawnZone.size = new Vector2(room.size * 2 - 3, room.size * 2 - 3);
                        enemySpawner.roomIndex = index;
                        enemySpawner.maxWave = 3;
                        enemySpawner.roomDifficult = GetRoomDifficult(index);
                        enemySpawner.enemyList = levelsList[currLevel].enemyList;
                        enemySpawner.roomInfo = room;
                        break;
                    case RoomTypes.FinalRoom:
                        enemySpawner = Instantiate(this.enemySpawner, parentSpawners);
                        enemySpawner.transform.position = pointV3 + offsetV3;
                        enemySpawner.spawnZone.size = new Vector2(room.size * 2 - 3, room.size * 2 - 3);
                        enemySpawner.roomIndex = index;
                        enemySpawner.maxWave = 1;
                        enemySpawner.roomDifficult = GetRoomDifficult(index);
                        enemySpawner.bossRoom = true;
                        enemySpawner.enemyList.Add(levelsList[currLevel].levelBoss);
                        enemySpawner.roomInfo = room;
                        break;
                    case RoomTypes.AdditionalRoom:
                        if (boostersList.Count == 0) break;
                        boosterGO = Instantiate(boostersList[Random.Range(0, boostersList.Count - 1)], tempFolder);
                        boosterGO.transform.position = pointV3 + offsetV3;
                        break;
                }
            }

            void FillObstacle()
            {
                if (room.roomType != RoomTypes.EnemyRoom) return;

                bool[,] mapObstacle = new bool[spacingRoom, spacingRoom];
                int countCells = (room.size * 2 - 1) * (room.size * 2 - 1);
                int mustFillCells = countCells * fillObstaclePercent / 100;

                Vector3 offset = new Vector3(-room.size + (float)1.5, -room.size + (float)1.5, 0);
                for (int i = 0; i < mustFillCells; i++)
                {
                    Vector2Int point = new Vector2Int();
                    point.Set(Random.Range(0, room.size * 2 - 1), Random.Range(0, room.size * 2 - 1));
                    if (mapObstacle[point.x, point.y])
                    {
                        List<Vector2Int> checkingPoints = new List<Vector2Int>();
                        CollectCheckablePoints(point);
                        for (int j = 0; j < checkingPoints.Count; j++)
                        {
                            if (!mapObstacle[checkingPoints[j].x, checkingPoints[j].y])
                            {
                                point.Set(checkingPoints[j].x, checkingPoints[j].y);
                                break;
                            }
                            else
                            {
                                CollectCheckablePoints(checkingPoints[j]);
                            }
                        }

                        // Local Methots
                        void CollectCheckablePoints(Vector2Int point)
                        {
                            Vector2Int newPoint = new Vector2Int(point.x, point.y + 1);
                            if (0 <= newPoint.y && newPoint.y < room.size * 2 - 1)
                                if (!checkingPoints.Contains(newPoint))
                                    checkingPoints.Add(newPoint);

                            newPoint = new Vector2Int(point.x + 1, point.y);
                            if (0 <= newPoint.x && newPoint.x < room.size * 2 - 1)
                                if (!checkingPoints.Contains(newPoint))
                                    checkingPoints.Add(newPoint);

                            newPoint = new Vector2Int(point.x, point.y - 1);
                            if (0 <= newPoint.y && newPoint.y < room.size * 2 - 1)
                                if (!checkingPoints.Contains(newPoint))
                                    checkingPoints.Add(newPoint);

                            newPoint = new Vector2Int(point.x - 1, point.y);
                            if (0 <= newPoint.x && newPoint.x < room.size * 2 - 1)
                                if (!checkingPoints.Contains(newPoint))
                                    checkingPoints.Add(newPoint);
                        }
                    }

                    mapObstacle[point.x, point.y] = true;
                    Vector3 spawnPoint = new Vector3(offsetRoom.x, offsetRoom.y, 0);
                    spawnPoint += new Vector3(point.x, point.y, 0);
                    GameObject obstaclePrefab;
                    if (point.x == 0 || point.y == 0 || point.x == room.size * 2 - 2 || point.y == room.size * 2 - 2)
                        obstaclePrefab = obstacleNearWallList[Random.Range(0, obstacleNearWallList.Count)];
                    else
                        obstaclePrefab = obstacleList[Random.Range(0, obstacleList.Count)];

                    Instantiate(obstaclePrefab, spawnPoint + offset, Quaternion.identity, obstacleFolder);
                }

                room.mapObstacle = mapObstacle;
            }
        }

        void BakeNavMesh()
        {
            navSurface1.BuildNavMesh();
            navSurface2.BuildNavMesh();
        }
    }

    private void OnBossRoomClear()
    {
        if (currLevel == levelsList.Length - 1)
        {
            GlobalEvents.SendVictoryNotification();
        }
        else
        {
            Room lastRoom = currRooms[currRooms.Count - 1];
            foreach (var room in currRooms)
            {
                if (room.roomType == RoomTypes.FinalRoom)
                {
                    lastRoom = room;
                    break;
                }
            }
            Vector3 offset = new Vector3(spacingRoom * lastRoom.position.x, spacingRoom * lastRoom.position.y);
            Instantiate(prefabLoadNextLevel, offset, Quaternion.identity, parentSpawners);
        }
    }

    private void OnLoadNextLevel()
    {
        DeleteOldLevel();

        void DeleteOldLevel()
        {
            ClearTilemaps();
            ClearAllSpawners();
            ClearAllTrash();

            currLevel++;
            if (currLevel > levelsList.Length - 1) currLevel = levelsList.Length - 1;
            currRooms = CreateLevel();



            void ClearTilemaps()
            {
                tilemapFloor.ClearAllTiles();
                tilemapWall.ClearAllTiles();
                tilemapGate.ClearAllTiles();
                tilemapSidewallIn.ClearAllTiles();
                tilemapSidewallOut.ClearAllTiles();
            }

            void ClearAllSpawners()
            {
                for (int i = parentSpawners.childCount - 1; i > -1; i--)
                {
                    Destroy(parentSpawners.transform.GetChild(i).gameObject);
                }
            }

            void ClearAllTrash()
            {
                Transform obstacles = GameObject.Find("Obstacles").transform;
                List<GameObject> obstacleList = new List<GameObject>();
                for (int i = 0; i < obstacles.childCount; i++)
                    obstacleList.Add(obstacles.GetChild(i).gameObject);
                foreach (GameObject obstacle in obstacleList)
                    Destroy(obstacle.gameObject);

                Transform tempTransform = GameObject.Find("Temp").transform;
                List<GameObject> tempList = new List<GameObject>();
                for (int i = 0; i < tempTransform.childCount; i++)
                    tempList.Add(tempTransform.GetChild(i).gameObject);
                foreach (GameObject temp in tempList)
                    Destroy(temp.gameObject);
            }
        }
    }
}
