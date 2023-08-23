using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class DropSlot
{
    public List<GameObject> DropItemPrefab => _dropItemPrefab;
    [SerializeField] private List<GameObject> _dropItemPrefab;
    public int Min => _min;
    [SerializeField, Min(0)] private int _min;
    public int Max => _max;
    [SerializeField, Min(0)] private int _max;
    public bool OneItem => _oneItem;
    [SerializeField] private bool _oneItem;
}