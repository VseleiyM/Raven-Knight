using System.Collections;
using System.Collections.Generic;
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
        _rigidbody2D = playerInfo.Rigidbody2D;
        animator = playerInfo.Animator;
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
            playerInfo.Player.ActiveInvincibleEffect(invincibleDuration);
            foreach (var comp in playerInfo.Player.DisableComponents)
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

            if (!playerInfo.Player.IsDead)
                foreach (var comp in playerInfo.Player.DisableComponents)
                    comp.enabled = true;
        }
    }

    private IEnumerator Cooldown(float duration)
    {
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        isReady = true;
    }
}
