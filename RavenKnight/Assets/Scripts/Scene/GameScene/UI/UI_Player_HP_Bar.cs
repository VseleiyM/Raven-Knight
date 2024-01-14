using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class UI_Player_HP_Bar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private void OnEnable()
        {
            GlobalEvents.playerInit += OnPlayerInit;
            GlobalEvents.targetTakeDamage += OnPlayerTakeDamage;
        }

        private void OnDisable()
        {
            GlobalEvents.playerInit -= OnPlayerInit;
            GlobalEvents.targetTakeDamage -= OnPlayerTakeDamage;
        }
        private void OnPlayerInit(Target player)
        {
            UnitParameter healthParameter = player.ReturnParameter(ParametersList.Health);
            float currentPersentHealth = healthParameter.current / healthParameter.Max * 100;
            slider.value = currentPersentHealth;
        }

        private void OnPlayerTakeDamage(Target player, string tag)
        {
            if (tag == GameObjectTag.Player.ToString())
            {
                UnitParameter healthParameter = player.ReturnParameter(ParametersList.Health);
                float currentPersentHealth = healthParameter.current / healthParameter.Max * 100;
                slider.value = currentPersentHealth;
            }
        }

    }
}