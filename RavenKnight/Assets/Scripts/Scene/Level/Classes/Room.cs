using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GenerateLevel
{
    public class Room
    {
        public Vector2Int position;
        public int size;
        public RoomTypes roomType = RoomTypes.Empty;
        public bool[,] mapObstacle;

        public bool corridorUp = false;
        public bool corridorRight = false;
        public bool corridorDown = false;
        public bool corridorLeft = false;
    }
}
