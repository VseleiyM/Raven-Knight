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
        }
    }
}