using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Project.GenerateLevel;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField] private List<TileBase> tilesFloor;
    [SerializeField] private List<TileBase> tilesWall;
    [SerializeField] private List<TileBase> tilesSidewall;
    [Space(10)]
    [SerializeField] private Tilemap tilemapFloor;
    [SerializeField] private Tilemap tilemapWall;
    [SerializeField] private Tilemap tilemapSidewallIn;
    [SerializeField] private Tilemap tilemapSidewallOut;
    [Space(10)]
    [SerializeField, Min(1)] private int spacingRoom = 20;
    [SerializeField, Min(1)] private int lenghtMaze = 1;

    void Start()
    {
        Room[] rooms = CreateMaze(lenghtMaze);
        foreach (Room room in rooms)
        {
            CreateRoom(room);
        }


        Room[] CreateMaze(int lenghtMaze)
        {
            if (lenghtMaze < 1) lenghtMaze = 1;
            List<Room> rooms = new List<Room>();

            Vector2Int startPosition = new Vector2Int(0, 0);
            Room currRoom = new Room();
            currRoom.position = startPosition;
            currRoom.size = Random.Range(spacingRoom / 4, spacingRoom / 2);
            rooms.Add(currRoom);

            Room nextRoom;
            int currLenghtMaze = 0;
            while (currLenghtMaze < lenghtMaze)
            {
                nextRoom = new Room();
                bool repeat = true;
                bool finish = false;

                List<int> checkingDir = new List<int>();
                while (repeat)
                {
                    repeat = false;

                    int direction = Random.Range(0, 4);
                    int countDir = 0;
                    foreach (int dir in checkingDir) 
                    {
                        if (dir == direction) direction++;
                        if (direction >= 4) direction = 0;
                        countDir++;
                        if (countDir >= 4) break;
                    }
                    checkingDir.Add(direction);

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

                    if (checkingDir.Count >= 5) finish = true;
                    if (!finish)
                    {
                        foreach (Room room in rooms)
                        {
                            if (room.position == nextRoom.position)
                            {
                                repeat = true;
                                break;
                            }
                        }

                        if (!repeat)
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
                    }
                }

                if (!finish)
                {
                    currLenghtMaze++;
                    nextRoom.size = Random.Range(spacingRoom / 4, spacingRoom / 2);
                    currRoom = nextRoom;
                    rooms.Add(currRoom);
                }
                else
                {
                    break;
                }
            }

            return rooms.ToArray();
        }

        void CreateRoom(Room room)
        {
            Vector2Int offset = new Vector2Int(spacingRoom * room.position.x, spacingRoom * room.position.y);

            CreateFloor(room.size, offset);
            CreateWall(room.size, offset);
            CreateCorridor(room, offset);


            void CreateFloor(int size, Vector2Int offset)
            {
                for (int y = size; y >= -size; y--)
                {
                    for (int x = -size; x <= size; x++)
                    {
                        TileBase tile = tilesFloor[Random.Range(0, tilesFloor.Count)];
                        tilemapFloor.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), tile);
                    }
                }
            }

            void CreateWall(int size, Vector2Int offset)
            {
                for (int x = -size; x <= size; x++)
                {
                    TileBase tile = tilesWall[Random.Range(0, tilesWall.Count)];
                    tilemapWall.SetTile(new Vector3Int(x + offset.x, size + offset.y, 0), tile);
                }
                for (int x = -size; x <= size; x++)
                {
                    TileBase tile = tilesWall[Random.Range(0, tilesWall.Count)];
                    tilemapWall.SetTile(new Vector3Int(x + offset.x, -size + offset.y, 0), tile);
                }
                for (int y = -size; y <= size; y++)
                {
                    TileBase tile = tilesWall[Random.Range(0, tilesWall.Count)];
                    tilemapWall.SetTile(new Vector3Int(-size + offset.x, y + offset.y, 0), tile);
                }
                for (int y = -size; y <= size; y++)
                {
                    TileBase tile = tilesWall[Random.Range(0, tilesWall.Count)];
                    tilemapWall.SetTile(new Vector3Int(size + offset.x, y + offset.y, 0), tile);
                }
            }

            void CreateCorridor(Room room, Vector2Int offset)
            {
                if (room.corridorUp)
                {
                    for (int y = 0; y <= spacingRoom; y++)
                    {
                        for (int x = -1; x <= 1; x++)
                        {
                            TileBase tile = tilesFloor[Random.Range(0, tilesWall.Count)];
                            tilemapFloor.SetTile(new Vector3Int(offset.x + x, offset.y + y, 0), tile);
                        }
                    }
                }
                if (room.corridorDown)
                {
                    for (int y = 0; y >= -spacingRoom; y--)
                    {
                        for (int x = -1; x <= 1; x++)
                        {
                            TileBase tile = tilesFloor[Random.Range(0, tilesWall.Count)];
                            tilemapFloor.SetTile(new Vector3Int(offset.x + x, offset.y + y, 0), tile);
                        }
                    }
                }
                if (room.corridorRight)
                {
                    for (int x = 0; x <= spacingRoom; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            TileBase tile = tilesFloor[Random.Range(0, tilesWall.Count)];
                            tilemapFloor.SetTile(new Vector3Int(offset.x + x, offset.y + y, 0), tile);
                        }
                    }
                }
                if (room.corridorLeft)
                {
                    for (int x = 0; x >= -spacingRoom; x--)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            TileBase tile = tilesFloor[Random.Range(0, tilesWall.Count)];
                            tilemapFloor.SetTile(new Vector3Int(offset.x + x, offset.y + y, 0), tile);
                        }
                    }
                }
            }
        }
    }
}
