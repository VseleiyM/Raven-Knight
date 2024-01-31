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
        [SerializeField] private AudioClip travelMusic2;
        [SerializeField] private AudioClip combatMusic;
        [SerializeField] private AudioClip bossMusic;
        [Space(10)]
        [SerializeField, Min(0.01f)] private float timeAttenuation = 1;
        [SerializeField, Min(0.01f)] private float soundAmplification = 1;
        [Space(10)]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource musicSource2;

        private float musicTimer;
        private float musicTimer2;

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

            switch (type)
            {
                case MusicType.combatMusic:
                    musicTimer = musicSource.time;
                    break;
                case MusicType.bossMusic:
                    musicTimer = musicSource.time;
                    break;
            }
            musicSource.Stop();
            musicSource2.Stop();
            switch (type)
            {
                case MusicType.mainMenu:
                    musicSource.clip = mainMenu;
                    break;
                case MusicType.travelMusic:
                    musicSource.clip = travelMusic;
                    musicSource2.clip = travelMusic2;
                    musicSource.time = musicTimer;
                    musicSource2.Play();
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