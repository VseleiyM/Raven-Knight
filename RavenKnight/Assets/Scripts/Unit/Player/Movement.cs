using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(PlayerInfo))]
    public class Movement : MonoBehaviour
    {
        private PlayerInfo playerInfo;
        private Rigidbody2D _rigidbody;
        private PCControl PCControl;

        public bool isFiring;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            playerInfo = GetComponent<PlayerInfo>();
            PCControl = GetComponent<PCControl>();
        }

        public void Move(Vector2 direction)
        {
            Vector2 offset = playerInfo.TargetInfo.Project(direction) * Time.fixedDeltaTime * playerInfo.Speed;
            _rigidbody.MovePosition(_rigidbody.position + offset);

            playerInfo.TargetInfo.Animator.SetBool(AnimatorParameter.Run.ToString(), direction.magnitude > 0);
        }

        public void LookDirection(Vector2 lookDir)
        {
            if (isFiring)
            {
                playerInfo.TargetInfo.SpriteRenderer.flipX = PCControl.LookVector.x < 0;
            }
            else
            {
                if (lookDir.x < 0)
                    playerInfo.TargetInfo.SpriteRenderer.flipX = true;
                else if (lookDir.x > 0)
                    playerInfo.TargetInfo.SpriteRenderer.flipX = false;
            }
        }
    }
}