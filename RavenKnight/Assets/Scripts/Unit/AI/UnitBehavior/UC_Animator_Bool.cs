using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class UC_Animator_Bool : UnitCommand
    {
        [SerializeField] private UnitCommand _nextStep;
        [Space(10)]
        [SerializeField] private AnimatorParameter paramName;
        [SerializeField] private bool value;

        private MobInfo mobInfo;

        public override UnitCommand NextStep => _nextStep;

        public override void Execute()
        {
            mobInfo.TargetInfo.Animator.SetBool(paramName.ToString(), value);
        }

        public override void RequestData(MobInfo mobInfo)
        {
            this.mobInfo = mobInfo;
        }
    }
}