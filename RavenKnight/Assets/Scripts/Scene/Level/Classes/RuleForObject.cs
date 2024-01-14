using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Project.GenerateLevel
{
    [CreateAssetMenu(fileName = "TileForGenerate", menuName = "GenerateLevel/TileForGenerate")]
    public class RuleForObject : ScriptableObject
    {
        public GameObject prefab;
        public bool oneRequirement;
        public Rule[] requirements;
    }
}