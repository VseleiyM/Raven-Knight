using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimDash : MonoBehaviour
{
    public Animator animator;
    private DashAbilitys dashAbility;


    private void Awake()
    {
        dashAbility = GetComponentInParent<DashAbilitys>();
        animator = GetComponentInChildren<Animator>();
    }

    public void StartDashAnimation()
    {
        animator.Play(dashAbility.dashAnimationName);
    }

    public void StartDefaultAnimation()
    {
        animator.Play(dashAbility.defaultAnimationName);
    }

    public void RotateObject(Vector2 movementDirection)
    {
   
        if (movementDirection == Vector2.zero) return;


        float rotationAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAngle));
    }

}
