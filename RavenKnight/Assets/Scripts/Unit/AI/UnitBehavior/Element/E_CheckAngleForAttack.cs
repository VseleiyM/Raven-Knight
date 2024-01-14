using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    [System.Serializable]
    public class E_CheckAngleForAttack
    {
        public float Min => _min;
        [SerializeField] private float _min;
        public float Max => _max;
        [SerializeField] private float _max;
        public UnitCommand NextStep => _nextStep;
        [SerializeField] private UnitCommand _nextStep;
    }
}