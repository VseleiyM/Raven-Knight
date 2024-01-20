using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    public class UI_VictoryNotification : MonoBehaviour
    {
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private GameObject epiloguePanel;
        [SerializeField] private GameObject epilogueExitText;
        [Header("Данные")]
        [SerializeField, Min(0f)] private float victoryDelay = 1;
        [SerializeField, Min(0f)] private float epilogueDelay = 1;
        [SerializeField] private bool exitEnable;

        private Coroutine corVictoryDelay;
        private Coroutine corEpilogueDelay;

        private void Update()
        {
            if (!exitEnable) return;
            if (Input.anyKeyDown)
            {
                GlobalEvents.SendChangeMusic(Audio.MusicType.mainMenu);
                GlobalEvents.SendReturnMenu();
            }
        }

        private void Awake()
        {
            victoryPanel.SetActive(false);
            epiloguePanel.SetActive(false);
            epilogueExitText.SetActive(false);

            GlobalEvents.victoryNotification += OnVictoryNotification;
        }

        private void OnDestroy()
        {
            GlobalEvents.victoryNotification -= OnVictoryNotification;
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnVictoryNotification()
        {
            gameObject.SetActive(true);
            victoryPanel.SetActive(true);
            corVictoryDelay = StartCoroutine(VictoryDelay(victoryDelay));
        }

        private IEnumerator VictoryDelay(float time)
        {
            float elapse = 0;
            while (elapse < time)
            {
                elapse += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            victoryPanel.SetActive(false);
            epiloguePanel.SetActive(true);
            corEpilogueDelay = StartCoroutine(EpilogueDelay(epilogueDelay));
        }

        private IEnumerator EpilogueDelay(float time)
        {
            float elapse = 0;
            while (elapse < time)
            {
                elapse += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            epilogueExitText.SetActive(true);
            exitEnable = true;
        }
    }
}