using System.Collections;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Ability Settings")]
    public string abilityName;
    public float cooldownTime = 1f;
    public bool IsActive { get; set; } = false;

    private float _timePassed = 0f;

    protected virtual void Start() { }

    protected virtual void FixedUpdate()
    {
        if (IsActive)
        {
            _timePassed += Time.fixedDeltaTime;
            if (_timePassed >= cooldownTime)
            {
                _timePassed = 0f;
                IsActive = false;
            }
        }
    }

    public virtual void UseAbility()
    {
        if (!IsActive)
        {
            Debug.Log($"Using ability: {abilityName}");
            

            IsActive = true;
        }
    }
}

