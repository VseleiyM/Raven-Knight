using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class UC_CheckAngleForAttack : UnitCommand
    {
        [SerializeField] private List<E_CheckAngleForAttack> checkSector;

        private MobInfo mobInfo;
        private UnitCommand _nextStep;

        public override UnitCommand NextStep => _nextStep;

        public override void Execute()
        {
            Vector3 startPoint = mobInfo.transform.position;
            Vector3 target = mobInfo.target.position;
            float angle = Mathf.Atan2(target.y - startPoint.y, target.x - startPoint.x) * Mathf.Rad2Deg;

            _nextStep = this;
            foreach (var sector in checkSector)
            {
                if (angle >= sector.Min && angle < sector.Max)
                {
                    _nextStep = sector.NextStep;
                    break;
                }
            }
        }

        public override void RequestData(MobInfo mobInfo)
        {
            this.mobInfo = mobInfo;
        }
    }
}