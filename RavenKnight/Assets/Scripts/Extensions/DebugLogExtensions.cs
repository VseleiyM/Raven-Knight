using UnityEngine;

namespace Extensions
{
    public static class DebugLogExtensions
    {
        public static void ErrorFieldIsNotSet(string fieldName)
        {
            Debug.LogError($"Field {fieldName} is not set!");
        }
    }
}