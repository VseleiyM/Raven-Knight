using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class UI_HealPotion : MonoBehaviour
    {
        [SerializeField] private TypePickupItem pickupItem;
        [SerializeField] private GameObject healAreaPrefab;
        [SerializeField] private KeyCode keyCode;
        [SerializeField] private Text text;
        [Space(10)]
        [SerializeField] private bool isReady;
        [Header("Filler image")]
        [SerializeField] private FilledIcon filledIcon;

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

            void UseItem()//Зачем?
            {
                filledIcon.ToMinValue();
                isReady = false;
                text.color = new Color(1, 1, 1, 0.39f);
                Instantiate(healAreaPrefab, spawnPoint.position, Quaternion.identity);
            }
            /*
             * Почему так нельзя делать в часто (и потенциально часто) вызываемых методах:
             * Объявление метода внутри другого метода в C# может привести к дополнительным затратам ресурсов:
                1) При каждом вызове внешнего метода, внутренний метод будет заново объявляться. 
                    Это означает, что будет выделяться память под метод, создаваться таблицы метаданных и т.д.
                2) Создание делегата для внутреннего метода также может привести к дополнительным накладным расходам.
                3) JIT-компилятору придется каждый раз заново оптимизировать код внутреннего метода, что тоже требует ресурсов.
             */
        }

        private void OnPlayerInit(Target player)
        {
            spawnPoint = player.transform;
            filledIcon.ToMinValue();
        }

        public void OnItemHasPickup(PickupItem item)
        {
            if (item.TypePickupItem == pickupItem)
            {
                filledIcon.value += item.Value;
                if (filledIcon.isMax && !isReady)
                {
                    isReady = true;
                    text.color = new Color(1, 1, 1, 1);
                }
            }
        }
    }
}