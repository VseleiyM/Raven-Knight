using UnityEngine;

public enum LogicOperators
{
    [InspectorName("<")] Less = 0,
    [InspectorName(">")] More = 1,
    [InspectorName("<=")] LessOrEqual = 2,
    [InspectorName(">=")] MoreOrEqual = 3,
    [InspectorName("=")] Equal = 4
}
