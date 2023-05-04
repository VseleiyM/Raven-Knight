using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevle : MonoBehaviour
{
    public static TestLevle instance;

    public TestLevlePrefabs Prefabs { get => _prefabs; }
    [SerializeField] private TestLevlePrefabs _prefabs;
    public Transform CanvasWorldPosition { get => _canvasWorldPosition; }
    [SerializeField] private Transform _canvasWorldPosition;

    private void Awake()
    {
        instance = this;
    }
}
