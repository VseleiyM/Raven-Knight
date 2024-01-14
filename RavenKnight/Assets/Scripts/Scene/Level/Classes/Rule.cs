using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GenerateLevel
{
    [System.Serializable]
    public class Rule
    {
        public Vector2Int position;
        public TileStatus tileStatus;
    }
}