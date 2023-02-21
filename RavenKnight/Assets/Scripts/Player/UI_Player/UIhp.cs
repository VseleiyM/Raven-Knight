using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIhp : MonoBehaviour
{

    public Text bar;
    private HealthPlayer h;
    private float HP;

    public Text textFps;
    private float fps;

    public Text waveText;
    private SpawenEneme SE;
    private float Wave;

    [SerializeField] private bool flag = true;


    void Start()
    {
        h = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthPlayer>();
        SE = GameObject.FindGameObjectWithTag("Spawen").GetComponent<SpawenEneme>();
    }

    // Update is called once per frame
    void Update()
    {
        float HP = h.health;
        bar.text = "HP" + HP;

        Wave = SE.WaveCount;
        waveText.text = "Wave: " + Wave*-1;

        textFps.text = ("FPS: " + (int)fps);

        if (flag == true)
        {
            flag = false;
            StartCoroutine(Next());
        }

    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(1f);
        fps = 1.0f / Time.deltaTime;
        flag = true;
    }
}
