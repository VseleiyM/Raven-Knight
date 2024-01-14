using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class UC_Animator_Float : UnitCommand
    {
        [SerializeField] private UnitCommand _nextStep;
        [Space(10)]
        [SerializeField] private AnimatorParameter paramName;
        [SerializeField] private float value;

        private MobInfo mobInfo;

        public override UnitCommand NextStep => _nextStep;

        public override void Execute()
        {
            mobInfo.TargetInfo.Animator.SetFloat(paramName.ToString(), value);
        }

        public override void RequestData(MobInfo mobInfo)
        {
            this.mobInfo = mobInfo;
        }
    }
}