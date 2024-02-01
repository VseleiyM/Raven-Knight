using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class UI_DefeatNotification : MonoBehaviour
    {
        [SerializeField] private List<GameObject> disableList;

        private void Awake()
        {
            GlobalEvents.playerDead += OnPlayerDead;
        }

        private void OnDestroy()
        {
            GlobalEvents.playerDead -= OnPlayerDead;
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GlobalEvents.SendReturnMenu();
            }
        }

        private void OnPlayerDead(Target player)
        {
            gameObject.SetActive(true);
            foreach (var item in disableList)
            {
                item.SetActive(false);
            }
        }
    }
}