using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public UnityEngine.UI.Slider musicSlider;
    public UnityEngine.UI.Slider soundSlider;
    public UnityEngine.UI.Button newGameButton;
    public UnityEngine.UI.Button loadButton;
    public UnityEngine.UI.Button settingsButton;
    public UnityEngine.UI.Button exitButton;
    public UnityEngine.UI.Button menuButton;
    
    public AudioSource music;
    public AudioSource click;
    
    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = music.volume;
        soundSlider.value = click.volume;
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        soundSlider.onValueChanged.AddListener(OnSoundVolumeChanged);
        
        newGameButton.onClick.AddListener(Click);
        loadButton.onClick.AddListener(Click);
        settingsButton.onClick.AddListener(Click);
        exitButton.onClick.AddListener(Click);
        menuButton.onClick.AddListener(Click);
    }
    
    private void OnMusicVolumeChanged(float newVolume)
    {
        music.volume = newVolume;
    }
    
    private void OnSoundVolumeChanged(float newVolume)
    {
        click.volume = newVolume;
    }
    
    private void Click()
    {
        click.Play();
    }
}
