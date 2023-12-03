using UI;
using UnityEngine;
using UnityEngine.UI;

public class UI_DamagePotion : MonoBehaviour
{
    public static UI_DamagePotion instance;

    [SerializeField] private TypePickupItem pickupItem;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private KeyCode keyCode;
    [SerializeField] private Text text;
    [Space(10)]
    [SerializeField] private bool isReady;
    [Header("Filler image")]
    [SerializeField] private FilledIcon filledIcon;

    private Camera _mainCamera;
    private Transform player;
    private Transform folder;

    private void Awake()
    {
        instance = this;
        _mainCamera = Camera.main;
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
        if (Input.GetKeyDown(keyCode) && isReady)
            UseItem();

        void UseItem()
        {
            Vector3 lookPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 lookingVector = lookPoint - player.transform.position;
            float angle = Mathf.Atan2(lookingVector.y, lookingVector.x) * Mathf.Rad2Deg;

            filledIcon.ToMinValue();
            isReady = false;
            text.color = new Color(1, 1, 1, 0.39f);
            var projectile = Instantiate(projectilePrefab, player.transform.position, Quaternion.Euler(0, 0, angle));
            projectile.transform.parent = folder;
        }
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
