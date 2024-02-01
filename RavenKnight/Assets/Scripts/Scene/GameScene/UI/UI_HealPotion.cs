using UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class UI_HealPotion : MonoBehaviour
{
	public static UI_HealPotion instance;

	[SerializeField] private TypePickupItem pickupItem;
    public Text text;
    [Space(10)]
	public bool isReady;
    [Header("Filler image")]
	public FilledIcon filledIcon;

    private Transform spawnPoint;

	private void Awake()
	{
		instance = this;
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
