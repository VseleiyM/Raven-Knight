using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealPotion : MonoBehaviour
{
    public static UI_HealPotion instance;

    [SerializeField] private GameObject healAreaPrefab;
    [SerializeField] private KeyCode keyCode;
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    [Space(10)]
    [SerializeField] private int currentValue;
    [SerializeField] private int maxValue;
    [SerializeField] private bool isReady;

    private Transform spawnPoint;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        GlobalEvents.playerInit += OnPlayerInit;
    }

    private void OnDisable()
    {
        GlobalEvents.playerInit -= OnPlayerInit;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) && isReady)
            UseItem();

        void UseItem()
        {
            currentValue = 0;
            slider.value = 0;
            isReady = false;
            text.color = new Color(1, 1, 1, 0.39f);
            Instantiate(healAreaPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    private void OnPlayerInit(Player player)
    {
        spawnPoint = player.transform;
        slider.value = 0;
    }

    public void OnItemPickup(int value)
    {
        currentValue += value;
        slider.value = 100 * currentValue / maxValue;
        if (currentValue >= maxValue && !isReady)
        {
            currentValue = maxValue;
            isReady = true;
            text.color = new Color(1, 1, 1, 1);
        }
    }
}
