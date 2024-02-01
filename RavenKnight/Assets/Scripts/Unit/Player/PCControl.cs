using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCControl : MonoBehaviour
{
    public Vector3 LookVector => _lookVector;
    private Vector3 _lookVector;

    private Movement movement;
    private PlayerInfo playerInfo;
    [SerializeField] private DashAbility dashAbility;
    [SerializeField] private KeyCode abilityKey = KeyCode.Space;
    [SerializeField] private KeyCode invincible = KeyCode.I;
    
    //�������� �����
	[SerializeField] private GameObject projectilePrefab;
	[SerializeField] private Transform folder;

	[SerializeField] private KeyCode potionCastKey;

	// �������� ����� 
	[SerializeField] private GameObject healAreaPrefab;
	[SerializeField] private Transform spawnPoint;

	[SerializeField] private KeyCode potionCastKey_1;


	private float horizontal;
    private float vertical;
    private Camera _mainCamera;
    private Transform jointGun;
    public Vector2 Direction => _direction;
    private Vector2 _direction;

    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();
        movement = GetComponent<Movement>();
        _mainCamera = Camera.main;
        jointGun = playerInfo.JointGun;
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        _direction.Set(horizontal, vertical);
        _direction = _direction.normalized;

        movement.LookDirection(_direction);

        if (Input.GetMouseButton(0))
        {
            movement.isFiring = true;
            playerInfo.weapon.Shoot();
        }
        else
        {
            movement.isFiring = false;
        }

        if (Input.GetKeyDown(abilityKey))
            dashAbility.ActiveDash(_direction);

        if (Input.GetKeyDown(invincible))
            playerInfo.TargetInfo.Target.ChangeInvincible();

		if (Input.GetKeyDown(potionCastKey) && UI_DamagePotion.instance.isReady)
			CastPotion();

		if (Input.GetKeyDown(potionCastKey_1) && UI_HealPotion.instance.isReady)
			CastHealPotion();
	}

    private void FixedUpdate()
    {
        movement.Move(_direction);

        Vector3 lookPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _lookVector = lookPoint - jointGun.position;
        float angle = Mathf.Atan2(_lookVector.y, _lookVector.x) * Mathf.Rad2Deg;
        jointGun.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        var compWeapon = playerInfo.weapon;
		
		if (compWeapon.transform.position.x > transform.position.x)
		{

			jointGun.localScale = new Vector3(
				jointGun.localScale.x,
				Mathf.Abs(jointGun.localScale.y),
				jointGun.localScale.z
			);
		}
		else
		{

			jointGun.localScale = new Vector3(
				jointGun.localScale.x,
				-Mathf.Abs(jointGun.localScale.y),
				jointGun.localScale.z
			);
		}
	}

    // �������� 
	private void CastPotion()
	{
		Vector3 lookPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector3 lookingVector = lookPoint - jointGun.position;
		float angle = Mathf.Atan2(lookingVector.y, lookingVector.x) * Mathf.Rad2Deg;

		UI_DamagePotion.instance.filledIcon.ToMinValue();
		UI_DamagePotion.instance.isReady = false;
		UI_DamagePotion.instance.text.color = new Color(1, 1, 1, 0.39f);
		var projectile = Instantiate(projectilePrefab, jointGun.position, Quaternion.Euler(0, 0, angle));
		projectile.transform.parent = folder;
	}

	private void CastHealPotion()
	{
		UI_HealPotion.instance.filledIcon.ToMinValue();
		UI_HealPotion.instance.isReady = false;
		UI_HealPotion.instance.text.color = new Color(1, 1, 1, 0.39f);
		Instantiate(healAreaPrefab, spawnPoint.position, Quaternion.identity);
	}
}
