using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.GenerateLevel
{
    [System.Serializable]
    public class LevelDifficult
    {
        [HideInInspector] public string name;
        public int lengthMaze;
        public int additionalRoom;
        public int difficultScore;
        public List<GameObject> enemyList;
        public GameObject levelBoss;
    }
}