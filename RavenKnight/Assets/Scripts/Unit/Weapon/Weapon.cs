using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Project
{
    public class Weapon : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer { get => _spriteRenderer; }
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField, Min(0.01f)] private float rateOfFire = 1;
        [SerializeField, Min(0f)] private float damage = 1;
        [SerializeField, Min(0)] private int additionalProjectile = 0;
        [SerializeField, Min(0)] private float projectileAngle = 0;
        [SerializeField, Min(0)] private float splashRadius;
        [SerializeField] private float splashDamage;

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

        private void OnEnable()
        {
            GlobalEvents.itemHasPickup += OnItemHasPickup;
        }

        private void OnDisable()
        {
            GlobalEvents.itemHasPickup -= OnItemHasPickup;
        }

        public void Shoot()
        {
            if (!isReady || Time.timeScale == 0) return;

            CreateProjectile(transform.rotation);
            CreateAdditionalProjectile();
            ChangeData();
            StartCoroutine(Cooldown());

            void CreateProjectile(Quaternion angle)
            {
                var projectile = Instantiate(projectilePrefab, transform.position, angle, temp);
                projectile.layer = (int)LayerName.PlayerTrigger;
                var compProjectile = projectile.GetComponent<Projectile>();
                if (compProjectile.AttackInfo.Splash)
                    compProjectile.AttackInfo.Splash.gameObject.layer = (int)LayerName.PlayerTrigger;
                compProjectile.damageableTag = playerInfo.DamageableTag;
                compProjectile.damage = damage;
                compProjectile.splashRadius = splashRadius;
                compProjectile.splashDamage = splashDamage;
            }

            void CreateAdditionalProjectile()
            {
                for (int i = 0; i < additionalProjectile; i++)
                {
                    Vector3 euler = transform.eulerAngles;
                    Vector3 offset = Vector3.forward * (i + 1) * projectileAngle;
                    Quaternion angleLeft = Quaternion.Euler(euler + offset);
                    Quaternion angleRight = Quaternion.Euler(euler - offset);
                    CreateProjectile(angleLeft);
                    CreateProjectile(angleRight);
                }
            }

            void ChangeData()
            {
                isReady = false;
                weaponInfo.Animator.SetTrigger("FireT");
            }
        }

        private void OnItemHasPickup(PickupItem item)
        {
            if (item.TypePickupItem == TypePickupItem.MultiShot)
            {
                int value = Mathf.CeilToInt(item.Value);
                additionalProjectile += value;
                return;
            }
            if (item.TypePickupItem == TypePickupItem.RateOfFire)
            {
                rateOfFire += item.Value;
            }
            if (item.TypePickupItem == TypePickupItem.SplashShot)
            {
                splashRadius += item.Value;
            }
            if (item.TypePickupItem == TypePickupItem.DamageBoost)
            {
                damage += item.Value;
            }
        }

        public void SoundEffect(AudioClip clip)
        {
            weaponInfo.AudioSource.clip = clip;
            weaponInfo.AudioSource.Play();
        }

        private IEnumerator Cooldown()
        {
            float cooldown = 1 / rateOfFire;
            yield return new WaitForSeconds(cooldown);
            isReady = true;
        }
    }
}