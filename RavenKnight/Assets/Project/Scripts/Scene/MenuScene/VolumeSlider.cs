using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string nameVolume;
    [SerializeField] private Text textField;

    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat(nameVolume, Mathf.Lerp(-40, 0, volume));
        if (volume == 0)
        {
            audioMixer.SetFloat(nameVolume, -80);
        }
        textField.text = $"{(int)(volume * 100)}%";
    }
}
