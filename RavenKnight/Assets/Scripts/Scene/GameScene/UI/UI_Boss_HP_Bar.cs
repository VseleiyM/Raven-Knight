using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project.UI
{
    public class UI_Boss_HP_Bar : MonoBehaviour
    {
        [SerializeField] private List<Slider> listSliders;
        [SerializeField] private TextMeshProUGUI nameBoss;

        private void Awake()
        {
            GlobalEvents.bossInit += OnBossInit;
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            GlobalEvents.bossInit -= OnBossInit;
        }

        private void OnEnable()
        {
            GlobalEvents.bossTakeDamage += OnBossTakeDamage;
            GlobalEvents.bossDead += OnBossDead;
        }

        private void OnDisable()
        {
            GlobalEvents.bossTakeDamage -= OnBossTakeDamage;
            GlobalEvents.bossDead -= OnBossDead;
        }

        private void OnBossInit(Target target)
        {
            gameObject.SetActive(true);

            UnitParameter healthParameter = target.ReturnParameter(ParametersList.Health);

            float currentPersentHealth = healthParameter.current / healthParameter.Max * 100;
            foreach (var slider in listSliders)
            {
                slider.value = currentPersentHealth;
            }

            nameBoss.text = target.gameObject.name.Replace("(Clone)", "");
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
            gameObject.SetActive(false);
        }
    }
}