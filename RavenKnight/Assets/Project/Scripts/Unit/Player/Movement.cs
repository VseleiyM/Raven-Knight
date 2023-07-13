﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInfo))]

public class Movement : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private Rigidbody2D _rigidbody;
    private MousePosition mousePosition;

    public bool isFiring;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        playerInfo = GetComponent<PlayerInfo>();
        mousePosition = GetComponent<MousePosition>();
    }

    public void Move(Vector2 direction)
    {
        Init();
        Animation();

        void Init()
        {
            Vector2 offset = direction * Time.fixedDeltaTime * playerInfo.Player.Speed;
            _rigidbody.MovePosition(_rigidbody.position + offset);
        }

        void Animation()
        {
            playerInfo.Animator.SetBool("Run", direction.magnitude > 0);
        }
    }

    public void LookDirection(Vector2 lookDir)
    {
        if (isFiring)
        {
            playerInfo.SpriteRenderer.flipX = mousePosition.LookVector.x < 0;
        }
        else
        {
            if (lookDir.x < 0)
                playerInfo.SpriteRenderer.flipX = true;
            else if (lookDir.x > 0)
                playerInfo.SpriteRenderer.flipX = false;
        }
    }
}
