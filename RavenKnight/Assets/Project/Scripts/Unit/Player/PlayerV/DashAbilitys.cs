using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DashAbilitys : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Rigidbody2D _rigidbody;
    private Animator animator;
    public Transform childTransform;
    private Animator childAnimator;
    public float cooldown = 1f;
    public float nextActivationTime = 0f;
    public float moveTime = 0.5f;
    public float dashSpeed = 6f;

    private void Awake()
    {
        playerInfo = GetComponentInParent<PlayerInfo>();
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        animator = playerInfo.Animator;
        childAnimator = playerInfo.ChildAnimator;
    } 

    public void TakeDash(Vector2 movementDirection)// dash
    {
        if (movementDirection == Vector2.zero) return;

        Activate();
        CanActivate();

        void Activate()
        {
            if (CanActivate())
            {
                StartCoroutine(MoveOverSeconds(this.gameObject, movementDirection, moveTime));
                nextActivationTime = Time.time + cooldown;
            }
        }

        bool CanActivate()
        {
            return Time.time >= nextActivationTime; 
        }

    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 Direction, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        Vector2 end = startingPos + Direction * (dashSpeed * seconds) ;

        void ChildDash()
        {
            float rotationAngle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            childTransform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAngle));
        }
        void DashStart()
        {
            animator.SetBool("Dash", true);
            childAnimator.SetBool("Dash", true);
            foreach (var comp in playerInfo.Player.DisableComponents)
                comp.enabled = false;
        }
        void DashExit()
        {
            childAnimator.SetBool("Dash", false);
            animator.SetBool("Dash", false);
            animator.Play("Ran");
            childAnimator.Play("Def");
            foreach (var comp in playerInfo.Player.DisableComponents)
                comp.enabled = true;
        }

        DashStart();

        while (elapsedTime < seconds)
        {
            Vector2 currentPos = _rigidbody.position;
            Vector2 newPos = Vector2.MoveTowards(currentPos, end, dashSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPos);
            elapsedTime += Time.fixedDeltaTime;
            ChildDash();
            yield return new WaitForFixedUpdate();
        }

        DashExit();
    }
}

