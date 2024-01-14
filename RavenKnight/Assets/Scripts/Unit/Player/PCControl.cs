using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class PCControl : MonoBehaviour
    {
        public Vector3 LookVector => _lookVector;
        private Vector3 _lookVector;

        private Movement movement;
        private PlayerInfo playerInfo;
        [SerializeField] private DashAbility dashAbility;
        [SerializeField] private KeyCode abilityKey = KeyCode.Space;
        [SerializeField] private KeyCode invincible = KeyCode.I;

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
        }

        private void FixedUpdate()
        {
            movement.Move(_direction);

            Vector3 lookPoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _lookVector = lookPoint - transform.position;
            float angle = Mathf.Atan2(_lookVector.y, _lookVector.x) * Mathf.Rad2Deg;
            jointGun.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            var compWeapon = playerInfo.weapon;
            if (compWeapon.transform.position.x > transform.position.x)
            {
                compWeapon.SpriteRenderer.flipY = false;
            }
            else
            {
                compWeapon.SpriteRenderer.flipY = true;
            }
        }
    }
}