using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealPotion : MonoBehaviour
{
    [SerializeField] private TypePickupItem pickupItem;
    [SerializeField] private GameObject healAreaPrefab;
    [SerializeField] private KeyCode keyCode;
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    [Space(10)]
    [SerializeField] private bool isReady;

    private Transform spawnPoint;

    private void OnEnable()
    {
        GlobalEvents.playerInit += OnPlayerInit;
        GlobalEvents.itemHasPickup += OnItemHasPickup;
    }

    private void OnDisable()
    {
        GlobalEvents.playerInit -= OnPlayerInit;
        GlobalEvents.itemHasPickup -= OnItemHasPickup;
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) && isReady)
            UseItem();

        void UseItem()
        {
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

    public void OnItemHasPickup(PickupItem item)
    {
        if (item.TypePickupItem != pickupItem) return;

        slider.value += item.Value;
        if (slider.value >= slider.maxValue && !isReady)
        {
            isReady = true;
            text.color = new Color(1, 1, 1, 1);
        }
    }
}
