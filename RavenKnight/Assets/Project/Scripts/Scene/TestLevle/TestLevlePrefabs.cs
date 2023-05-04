using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevlePrefabs : MonoBehaviour
{
    public GameObject TextScore { get => _textScore; }
    [SerializeField] private GameObject _textScore;
}
