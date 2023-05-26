using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player_HP_Bar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void OnEnable()
    {
        GlobalEvents.playerInit += OnPlayerInit;
        GlobalEvents.playerTakeDamage += OnPlayerTakeDamage;
    }

    private void OnDisable()
    {
        GlobalEvents.playerInit -= OnPlayerInit;
        GlobalEvents.playerTakeDamage -= OnPlayerTakeDamage;
    }
    private void OnPlayerInit(Player player)
    {
        float currentPersentHealth = player.CurrentHealth / player.MaxHealth * 100;
        slider.value = currentPersentHealth;
    }

    private void OnPlayerTakeDamage(Player player)
    {
        float currentPersentHealth = player.CurrentHealth / player.MaxHealth * 100;
        slider.value = currentPersentHealth;
    }

}
