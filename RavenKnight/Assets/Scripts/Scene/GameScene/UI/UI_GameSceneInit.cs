using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    public class UI_GameSceneInit : MonoBehaviour
    {
        [SerializeField] private UI_DefeatNotification defeatNotification;
        [SerializeField] private UI_VictoryNotification victoryNotification;

        private void Awake()
        {
            defeatNotification.gameObject.SetActive(true);  
            victoryNotification.gameObject.SetActive(true);
        }
    }
}