using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionController : MonoBehaviour
{
    public Dropdown Resolution;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change()
    {
        if (Resolution.value == 0)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if (Resolution.value == 1)
        {
            Screen.SetResolution(1366, 768, true);
        }
        else if (Resolution.value == 2)
        {
            Screen.SetResolution(1024, 768, true);
        }
    }

    public void ChangeWindowMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
