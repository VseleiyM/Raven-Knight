using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class DashAbilityNew : MonoBehaviour
{
    [SerializeField] private float cooldown = 1f;
    [SerializeField, Min(0.01f)] private float duration = 0.2f;
    [SerializeField] private float distance = 2;
    [SerializeField] private float invincibleDuration = 0.2f;

    private PlayerInfo playerInfo;
    private Rigidbody2D _rigidbody2D;
    private Animator animator;
    private Animator dashAnimator;
    private ParticleSystem particle;
    private TrailRenderer trailRenderer;

    private bool isReady = true;

    private void Awake()
    {
        playerInfo = GetComponentInParent<PlayerInfo>();
        _rigidbody2D = playerInfo.TargetInfo.Rigidbody2D;
        animator = playerInfo.TargetInfo.Animator;
        dashAnimator = GetComponent<Animator>();
        particle = GetComponentInChildren<ParticleSystem>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    public void ActiveDash(Vector2 direction)
    {
        if (direction == Vector2.zero) return;

        if (isReady)
        {
            isReady = false;
            StartCoroutine(Init(direction));
            StartCoroutine(Cooldown(cooldown));
        }
    }

    private IEnumerator Init(Vector2 direction)
    {
        DashStart();
        float curDuration = duration;
        float speed = distance / duration;
        ParticleEffect();
        while (curDuration > 0)
        {
            Vector2 offset = direction * (speed * Time.fixedDeltaTime);
            _rigidbody2D.MovePosition(_rigidbody2D.position + offset);
            curDuration -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        DashExit();


        void DashStart()
        {
            animator.SetBool(AnimatorParameter.Dash.ToString(), true);
            playerInfo.TargetInfo.Target.ActiveInvincibleEffect(invincibleDuration);
            foreach (var comp in playerInfo.DisableComponents)
                comp.enabled = false;
        }

        void ParticleEffect()
        {
            particle.Play();
            trailRenderer.emitting = true;
            if (direction.x < 0)
            {
                particle.transform.rotation = Quaternion.Euler(0, 90, 0);
                dashAnimator.SetBool(AnimatorParameter.ParticleFlip.ToString(), true);
            }
            else if (direction.x > 0)
            {
                particle.transform.rotation = Quaternion.Euler(0, -90, 0);
                dashAnimator.SetBool(AnimatorParameter.ParticleFlip.ToString(), false);
            }
        }

        void DashExit()
        {
            animator.SetBool(AnimatorParameter.Dash.ToString(), false);
            particle.Stop();
            trailRenderer.emitting = false;

            if (!playerInfo.IsDead)
                foreach (var comp in playerInfo.DisableComponents)
                    comp.enabled = true;
        }
    }

    private IEnumerator Cooldown(float duration)
    {
        DashProgressView dashView = DashProgressView.instance;
        dashView.SetReady(false);
        while (duration > 0)
        {
            dashView.filledValue = (cooldown - duration) / cooldown;
            duration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        dashView.filledValue = 1;
        dashView.SetReady(true);
        isReady = true;
    }
}
