using UnityEngine;

[System.Serializable]
public class UnitParameter
{
    [HideInInspector] public string name;
    public ParametersList Parameter => _parameter;
    [SerializeField] private ParametersList _parameter;
    [SerializeField] public float current;
    public float Max => _max;
    [SerializeField] private float _max;
    public bool FillAwake => _fillAwake;
    [SerializeField] private bool _fillAwake;
}
