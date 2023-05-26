using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private Transform folder;

    private void Awake()
    {
        var goFolder = GameObject.Find("Units");
        if (!goFolder)
            folder = new GameObject("Units").transform;
        else
            folder = goFolder.transform;
    }

    private void Start()
    {
        var player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        player.transform.parent = folder;
    }
}
