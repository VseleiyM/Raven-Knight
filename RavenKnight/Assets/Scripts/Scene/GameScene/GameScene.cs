using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public static GameScene instance;

    public Transform CanvasWorldPosition { get => _canvasWorldPosition; }
    [SerializeField] private Transform _canvasWorldPosition;

    private void Awake()
    {
        instance = this;
    }
}
