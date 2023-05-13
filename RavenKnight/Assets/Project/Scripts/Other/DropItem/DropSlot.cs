using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class DropSlot
{
    public GameObject DropItemPrefab => _dropItemPrefab;
    [SerializeField] private GameObject _dropItemPrefab;
    public int DropChance => _dropChance;
    [SerializeField] [Min(0)] private int _dropChance;
}