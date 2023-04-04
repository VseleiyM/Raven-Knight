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

    private PlayerInfo playerInfo;
    private bool isReady = true;

    private void Awake()
    {
        playerInfo = GetComponentInParent<PlayerInfo>();
        playerInfo.weapon = this;
    }

    public void Shoot()
    {
        if (isReady)
        {
            var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.layer = playerInfo.gameObject.layer;
            var compProjectile = projectile.GetComponent<Projectile>();
            compProjectile.damageableTag = playerInfo.Player.DamageableTag;
            isReady = false;
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }
}
