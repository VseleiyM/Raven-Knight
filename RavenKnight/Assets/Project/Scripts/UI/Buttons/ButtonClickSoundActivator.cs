using Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class ButtonClickSoundActivator : MonoBehaviour
    {
        [Inject] private AudioController audioController;
        private AudioSource clickSound
        {
            get => audioController.GetSource(AudioSourceType.buttonClick);
        }
        private void Awake()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => clickSound.Play());
        }
    }
}