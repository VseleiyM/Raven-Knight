using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Project
{
    public class MoveToSidestep : UnitCommand
    {
        [SerializeField] private UnitCommand _nextStep;
        [Space(10)]
        [SerializeField, Min(0.01f)] private float distance = 1;

        private MobInfo mobInfo;
        private UnitCommand curStep;
        private bool isMoving = false;
        private bool isFinish = false;
        private Vector3 destination;

        public override UnitCommand NextStep => curStep;

        public override void Execute()
        {
            if (isFinish)
            {
                curStep = _nextStep;
                isFinish = false;
                isMoving = false;
                return;
            }

            if (isMoving) return;

            curStep = this;
            isMoving = true;

            Vector2 target = mobInfo.target.position;
            Vector2 self = mobInfo.transform.position;
            Vector2 ray = self - target;
            Vector2 direction;
            if (Random.Range(0, 2) == 0)
                direction = new Vector2(-ray.y, ray.x).normalized;
            else
                direction = new Vector2(ray.y, -ray.x).normalized;

            mobInfo.Agent.isStopped = false;
            mobInfo.TargetInfo.Animator.SetBool("Run", true);
            mobInfo.TargetInfo.FlipModel(transform.position.x > mobInfo.target.transform.position.x);
            destination = self + direction * distance;
            mobInfo.Agent.SetDestination(destination);

            StartCoroutine(CheckStatus());
        }

        public override void RequestData(MobInfo mobInfo)
        {
            this.mobInfo = mobInfo;
        }

        private IEnumerator CheckStatus()
        {
            while (true)
            {
                float magnitude = (destination - mobInfo.transform.position).magnitude;
                if (magnitude < 0.6)
                {
                    isFinish = true;
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}