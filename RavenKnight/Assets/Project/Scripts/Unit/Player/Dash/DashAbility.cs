using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DashAbility : MonoBehaviour
{
    [SerializeField] private Transform tailTransform;
    [SerializeField] private float cooldown = 1f;
    [SerializeField, Min(0.01f)] private float duration = 0.2f;
    [SerializeField] private float speed = 20f;

    private PlayerInfo playerInfo;
    private Rigidbody2D _rigidbody2D;
    private Animator animator;
    private Animator dashAnimator;

    private bool isReady = true;

    private void Awake()
    {
        playerInfo = GetComponentInParent<PlayerInfo>();
        _rigidbody2D = playerInfo.Rigidbody2D;
        animator = playerInfo.Animator;
        dashAnimator = GetComponentInChildren<Animator>();
    }

    public void ActiveDash(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        if (isReady)
        {
            isReady = false;
            StartCoroutine(Init(direction, duration));
            StartCoroutine(Cooldown(cooldown));
        }
    }

    public IEnumerator Init(Vector2 direction, float duration)
    {
        DashStart();
        while (duration > 0)
        {
            ChildDash();
            Vector2 offset = direction * (speed * Time.fixedDeltaTime);
            _rigidbody2D.MovePosition(_rigidbody2D.position + offset);
            duration -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        DashExit();


        void DashStart()
        {
            animator.SetBool(AnimatorParameter.Dash.ToString(), true);
            dashAnimator.SetBool(AnimatorParameter.Dash.ToString(), true);
            foreach (var comp in playerInfo.Player.DisableComponents)
                comp.enabled = false;
        }

        void ChildDash()
        {
            float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            tailTransform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAngle));
        }

        void DashExit()
        {
            dashAnimator.SetBool(AnimatorParameter.Dash.ToString(), false);
            animator.SetBool(AnimatorParameter.Dash.ToString(), false);
            if (!playerInfo.Player.IsDead)
                foreach (var comp in playerInfo.Player.DisableComponents)
                    comp.enabled = true;
        }
    }

    private IEnumerator Cooldown(float duration)
    {
        while(duration > 0)
        {
            duration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        isReady = true;
    }
}

