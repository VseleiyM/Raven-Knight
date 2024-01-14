using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class UC_ChangeAgentParameters : UnitCommand
    {
        public override UnitCommand NextStep => _nextStep;
        [SerializeField] private UnitCommand _nextStep;

        [SerializeField] private float speed;
        [SerializeField] private float angularSpeed;
        [SerializeField] private float acceleration;

        private MobInfo _mobInfo;

        public override void Execute()
        {
            _mobInfo.Agent.speed = speed;
            _mobInfo.Agent.angularSpeed = angularSpeed;
            _mobInfo.Agent.acceleration = acceleration;
        }

        public override void RequestData(MobInfo mobInfo)
        {
            _mobInfo = mobInfo;
        }
    }
}