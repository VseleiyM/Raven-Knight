using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Boss_HP_Bar : MonoBehaviour
{
    [SerializeField] private List<Slider> listSliders;
    [SerializeField] private Text nameBoss;

    private void OnEnable()
    {
        GlobalEvents.bossInit += OnBossInit;
        GlobalEvents.bossTakeDamage += OnBossTakeDamage;
        GlobalEvents.bossDead += OnBossDead;
    }

    private void OnDisable()
    {
        GlobalEvents.bossInit -= OnBossInit;
        GlobalEvents.bossTakeDamage -= OnBossTakeDamage;
        GlobalEvents.bossDead -= OnBossDead;
    }

    private void OnBossInit(Mob mob)
    {
        foreach (var slider in listSliders)
        {
            slider.gameObject.SetActive(true);
        }
        nameBoss.gameObject.SetActive(true);

        float currentPersentHealth = mob.CurrentHealth / mob.MaxHealth * 100;
        foreach (var slider in listSliders)
        {
            slider.value = currentPersentHealth;
        }
    }

    private void OnBossTakeDamage(Mob mob)
    {
        float currentPersentHealth = mob.CurrentHealth / mob.MaxHealth * 100;
        foreach (var slider in listSliders)
        {
            slider.value = currentPersentHealth;
        }
    }

    private void OnBossDead(Mob mob)
    {
        foreach (var slider in listSliders)
        {
            slider.gameObject.SetActive(false);
        }
        nameBoss.gameObject.SetActive(false);
    }
}
