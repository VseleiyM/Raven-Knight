using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UC_ChangeParameter : UnitCommand
{
    [SerializeField] private UnitCommand _nextStep;
    [Space(10)]
    [SerializeField] private ParametersList parameterName;
    [SerializeField] private MathOperator mathOperator;
    [SerializeField] private float value;

    private UnitParameter parameter;
    private MobInfo mobInfo;

    public override UnitCommand NextStep => _nextStep;

    public override void Execute()
    {
        if (parameter != null)
        {
            switch (mathOperator)
            {
                case MathOperator.Plus:
                    parameter.current += value;
                    break;
                case MathOperator.Minus:
                    parameter.current -= value;
                    break;
                case MathOperator.Multiplay:
                    parameter.current *= value;
                    break;
                case MathOperator.Divaded:
                    parameter.current /= value;
                    break;
            }
            if (parameter.current > parameter.Max)
                parameter.current = parameter.Max;
        }
    }

    public override void RequestData(MobInfo mobInfo)
    {
        this.mobInfo = mobInfo;
        parameter = mobInfo.Mob.ReturnParameter(parameterName);
    }
}
