using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Audio
{
    public class MusicList : MonoBehaviour
    {
        [SerializeField] private AudioClip mainMenu;
        [SerializeField] private AudioClip travelMusic;
        [SerializeField] private AudioClip combatMusic;
        [SerializeField] private AudioClip bossMusic;
        [Space(10)]
        [SerializeField, Min(0.01f)] private float timeAttenuation = 1;
        [SerializeField, Min(0.01f)] private float soundAmplification = 1;
        [Space(10)]
        [SerializeField] private AudioSource musicSource;

        private void Awake()
        {
            GlobalEvents.changeMusic += OnChangeMusic;
        }

        private void Start()
        {
            GlobalEvents.SendChangeMusic(MusicType.mainMenu);
        }

        private void OnDestroy()
        {
            GlobalEvents.changeMusic -= OnChangeMusic;
        }

        private void OnChangeMusic(MusicType type)
        {
            StartCoroutine(ChangeMusic(type));
        }

        private IEnumerator ChangeMusic(MusicType type)
        {
            float timer = 0;
            while (timer < timeAttenuation)
            {
                timer += Time.fixedDeltaTime;
                musicSource.volume -= Time.fixedDeltaTime / timeAttenuation;
                yield return new WaitForFixedUpdate();
            }

            musicSource.Stop();
            switch (type)
            {
                case MusicType.mainMenu:
                    musicSource.clip = mainMenu;
                    break;
                case MusicType.travelMusic:
                    musicSource.clip = travelMusic;
                    break;
                case MusicType.combatMusic:
                    musicSource.clip = combatMusic;
                    break;
                case MusicType.bossMusic:
                    musicSource.clip = bossMusic;
                    break;
            }
            musicSource.Play();

            timer = 0;
            while (timer < soundAmplification)
            {
                timer += Time.fixedDeltaTime;
                musicSource.volume += Time.fixedDeltaTime / soundAmplification;
                yield return new WaitForFixedUpdate();
            }
            musicSource.volume = 1;
            
        }
    }
}