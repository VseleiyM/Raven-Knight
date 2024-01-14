using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public abstract class UnitCommand : MonoBehaviour
    {
        public abstract UnitCommand NextStep { get; }

        public abstract void RequestData(MobInfo mobInfo);

        public abstract void Execute();
    }
}