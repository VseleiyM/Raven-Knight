using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveConfigsController : MonoBehaviour
{
    private ConfigData _configData = new ConfigData();
    private string _path;
    
    public UnityEngine.UI.Slider musicSlider;
    public UnityEngine.UI.Slider soundSlider;
    public UnityEngine.UI.Button applyChangesButton;
    public AudioSource music;
    public AudioSource click;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _path = Path.Combine(Application.dataPath, "Config.json");
        applyChangesButton.onClick.AddListener(ApplyChanges);

        
        if (File.Exists(_path))
        {
            _configData = JsonUtility.FromJson<ConfigData>(File.ReadAllText(_path));
            music.volume = _configData.musicValue;
            click.volume = _configData.soundValue;
        }
        
        musicSlider.value = music.volume;
        soundSlider.value = click.volume;
    }

    // Update is called once per frame


    private void ApplyChanges()
    {
        _configData.soundValue = click.volume;
        _configData.musicValue = music.volume;
        File.WriteAllText(_path, JsonUtility.ToJson(_configData));
    }
}

[Serializable]
public class ConfigData
{
    public float soundValue;
    public float musicValue;
}