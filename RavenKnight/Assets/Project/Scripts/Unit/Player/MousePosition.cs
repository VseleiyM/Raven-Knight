using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{

    public Vector3 LookVector => _lookVector;
    private Vector3 _lookVector;

    private Camera _mainCamera;
    private PlayerInfo playerInfo;
    private Transform jointGun;

    private void Awake()
    {
        _mainCamera = Camera.main;
        playerInfo = GetComponent<PlayerInfo>();
        jointGun = playerInfo.JointGun;
    }

    private void FixedUpdate()
    {
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
