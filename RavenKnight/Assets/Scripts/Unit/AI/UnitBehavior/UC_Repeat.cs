using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class UC_Repeat : UnitCommand
    {
        [SerializeField] private UnitCommand repeat;
        [SerializeField] private UnitCommand complete;
        [Space(10)]
        [SerializeField] private int count;

        private int currentCount = 0;
        private UnitCommand _nextStep;
        private MobInfo mobInfo;

        public override UnitCommand NextStep => _nextStep;

        public override void Execute()
        {
            if (currentCount < count)
            {
                _nextStep = repeat;
                currentCount++;
            }
            else
            {
                _nextStep = complete;
                currentCount = 0;
            }
        }

        public override void RequestData(MobInfo mobInfo)
        {
            this.mobInfo = mobInfo;
        }
    }
}