using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;

        private Transform folder;
        private Transform player;

        private void Awake()
        {
            var goFolder = GameObject.Find("PlayerUnits");
            if (!goFolder)
                folder = new GameObject("PlayerUnits").transform;
            else
                folder = goFolder.transform;

            PlayerInfo playerInfo = folder.GetComponentInChildren<PlayerInfo>();
            if (playerInfo != null) player = playerInfo.transform;
        }

        private void Start()
        {
            if (this.player == null)
            {
                var player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
                player.transform.parent = folder;
            }
            else
            {
                player.position = transform.position;
            }
        }
    }
}