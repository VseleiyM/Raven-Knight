using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private SaveData _saveData = new SaveData();
    private string _path;
    

    public UnityEngine.UI.InputField textField;
    public UnityEngine.UI.Text testSave;
    public UnityEngine.UI.Button saveButton; 
    
    // Start is called before the first frame update
    void Start()
    {
        saveButton.onClick.AddListener(SaveData);
        _path = Path.Combine(Application.dataPath, "Save.json");

        if (File.Exists(_path))
        {
            _saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(_path));
            testSave.text = _saveData.textData;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SaveData()
    {
        _saveData.textData = textField.text;
        File.WriteAllText(_path, JsonUtility.ToJson(_saveData));
    }
}


[Serializable]
public class SaveData
{
    public string textData;
}