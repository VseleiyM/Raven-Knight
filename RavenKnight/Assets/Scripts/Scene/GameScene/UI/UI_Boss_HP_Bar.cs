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

    private void OnBossInit(Target target)
    {
        foreach (var slider in listSliders)
        {
            slider.gameObject.SetActive(true);
        }
        nameBoss.gameObject.SetActive(true);

        UnitParameter healthParameter = target.ReturnParameter(ParametersList.Health);
        
        float currentPersentHealth = healthParameter.current / healthParameter.Max * 100;
        foreach (var slider in listSliders)
        {
            slider.value = currentPersentHealth;
        }

        nameBoss.text = target.gameObject.name;
    }

    private void OnBossTakeDamage(Target target)
    {
        UnitParameter healthParameter = target.ReturnParameter(ParametersList.Health);

        float currentPersentHealth = healthParameter.current / healthParameter.Max * 100;
        foreach (var slider in listSliders)
        {
            slider.value = currentPersentHealth;
        }
    }

    private void OnBossDead(Target target)
    {
        foreach (var slider in listSliders)
        {
            slider.gameObject.SetActive(false);
        }
        nameBoss.gameObject.SetActive(false);
    }
}
