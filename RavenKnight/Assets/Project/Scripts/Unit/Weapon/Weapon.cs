using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; }
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject projectilePrefab;
    [Min(0f)]
    [SerializeField] private float cooldown = 1;
    [Min(0f)]
    [SerializeField] private float damage = 1;

    private PlayerInfo playerInfo;
    private WeaponInfo weaponInfo;
    private bool isReady = true;
    private Transform temp;

    private void Awake()
    {
        var goTemp = GameObject.Find("Temp");
        if (!goTemp)
            temp = new GameObject("Temp").transform;
        else
            temp = goTemp.transform;

        playerInfo = GetComponentInParent<PlayerInfo>();
        weaponInfo = GetComponent<WeaponInfo>();
        playerInfo.weapon = this;
    }

    public void Shoot()
    {
        if (!isReady) return;

        Init();
        ChangeData();
        StartCoroutine(Cooldown());

        void Init()
        {
            var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.transform.parent = temp;
            projectile.layer = (int)LayerName.PlayerProjectile;
            var compProjectile = projectile.GetComponent<Projectile>();
            compProjectile.damageableTag = playerInfo.Player.DamageableTag;
            compProjectile.damage = damage;
        }

        void ChangeData()
        {
            isReady = false;
            weaponInfo.Animator.SetTrigger("FireT");
        }
    }

    public void SoundEffect(AudioClip clip)
    {
        weaponInfo.AudioSource.clip = clip;
        weaponInfo.AudioSource.Play();
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }
}
