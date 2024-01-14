using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class UC_IF_Parameter : UnitCommand
    {
        public override UnitCommand NextStep => _nextStep;
        private UnitCommand _nextStep;

        [SerializeField] private UnitCommand ifTrue;
        [SerializeField] private UnitCommand ifFalse;
        [Space(10)]
        [SerializeField] private bool singleTriggering;
        [SerializeField] private ParametersList parameterName;
        [SerializeField] private LogicOperators logicOperator;
        [SerializeField] private float value;

        private UnitParameter checkableValue;
        private bool hasTriggered = false;

        public override void RequestData(MobInfo mobInfo)
        {
            checkableValue = mobInfo.TargetInfo.Target.ReturnParameter(parameterName);
        }

        public override void Execute()
        {
            if (checkableValue != null && !hasTriggered)
            {
                bool result = false;

                switch (logicOperator)
                {
                    case LogicOperators.Less:
                        result = checkableValue.current < value;
                        break;
                    case LogicOperators.More:
                        result = checkableValue.current > value;
                        break;
                    case LogicOperators.LessOrEqual:
                        result = checkableValue.current <= value;
                        break;
                    case LogicOperators.MoreOrEqual:
                        result = checkableValue.current >= value;
                        break;
                    case LogicOperators.Equal:
                        result = checkableValue.current == value;
                        break;
                }

                if (result)
                {
                    _nextStep = ifTrue;
                    if (singleTriggering)
                        hasTriggered = true;
                }
                else
                {
                    _nextStep = ifFalse;
                }
            }
            else
            {
                _nextStep = ifFalse;
            }
        }
    }
}