using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AuidoSourceWithType[] audioSourcesWithType = new AuidoSourceWithType[0];

        [Serializable]
        private class AuidoSourceWithType
        {
            public AudioSourceType type;
            public AudioSource source;
        }
        /// <summary>
        /// Получить источник звука по заданному типу.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public AudioSource GetSource(AudioSourceType type)
        {
            return audioSources[type];
        }
        private Dictionary<AudioSourceType, AudioSource> audioSources;
        private void Awake()
        {
            //Положить значения в словарь для быстрого доступа.
            audioSources = new Dictionary<AudioSourceType, AudioSource>(audioSourcesWithType.Length);
            AudioSourceType type;
            foreach (AuidoSourceWithType sourceWithType in audioSourcesWithType)
            {
                type = sourceWithType.type;
                if (type == AudioSourceType.unknown)
                {
                    Debug.LogError("Type is not set!");
                }

                //Проверить на наличие дубликатов, их не должно быть
                if (audioSources.ContainsKey(type))
                {
                    Debug.LogError($"Specified type ({type}) is duplicated!");
                }
                else
                {
                    //Если все ок, то добавить в словарь.
                    audioSources.Add(type, sourceWithType.source);
                }
            }
        }
    }
}