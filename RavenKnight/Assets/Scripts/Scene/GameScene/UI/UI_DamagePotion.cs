using UI;
using UnityEngine;
using UnityEngine.UI;

public class UI_DamagePotion : MonoBehaviour
{
    public static UI_DamagePotion instance;

    [SerializeField] private TypePickupItem pickupItem;
    public Text text;
    [Space(10)]
    public bool isReady;
    [Header("Filler image")]
    public FilledIcon filledIcon;

    private Transform player;
    private Transform folder;

    private void Awake()
    {
        instance = this;
        var goFolder = GameObject.Find("Temp");
        if (!goFolder)
            folder = new GameObject("Temp").transform;
        else
            folder = goFolder.transform;
    }

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

    }

    private void OnPlayerInit(Target target)
    {
        player = target.transform;
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
