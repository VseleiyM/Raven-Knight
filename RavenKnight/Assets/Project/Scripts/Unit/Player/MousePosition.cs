using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera viewCamera;
    private PlayerInfo playerInfo;

    private Transform jointGun;

    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();
        jointGun = playerInfo.JointGun;
    }

    private void FixedUpdate()
    {
        Vector3 lookPoint = viewCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookingVector = lookPoint - transform.position;
        float angle = Mathf.Atan2(lookingVector.y, lookingVector.x) * Mathf.Rad2Deg;
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
