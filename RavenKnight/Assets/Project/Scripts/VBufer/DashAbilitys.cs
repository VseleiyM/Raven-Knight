using UnityEngine;

public class DashAbilitys : Ability
{
    public float dashSpeed = 5f;
    public float dashDuration = 0.5f;
    public string dashAnimationName = "Dash";
    public string defaultAnimationName = "Default";
    public RotationAnimDash rotationAnimDash;

    private KeyboardInput _characterMovement;
    private Vector3 _dashDirection;
    private float _dashTime;
    private bool _isDashing;

    private void Awake()
    {
        _characterMovement = GetComponent<KeyboardInput>();
    }

    public void StartDashAnimation()
    {
        rotationAnimDash.StartDashAnimation();
    }

    public void StartDefaultAnimation()
    {
        rotationAnimDash.StartDefaultAnimation();
    }

   

    public override void UseAbility()
    {
        if (!IsActive)
        {
            base.UseAbility();
            StartDash();
        }
    }

    private void Update()
    {
        if (_isDashing)
        {
            _dashTime -= Time.deltaTime;
            if (_dashTime <= 0)
            {
                _isDashing = false;
                StartDefaultAnimation();
            }
            else
            {
                StartDashAnimation();
                transform.position += _dashDirection * dashSpeed * Time.deltaTime;
                //Debug.Log($"_dashDirection - {_dashDirection} , dashSpeed - {dashSpeed} , Time.deltaTime - {Time.deltaTime}");
            }
        }
        
    }

    private void StartDash()
    {
        if (IsActive)
        {
            _dashDirection = _characterMovement.Direction;
            rotationAnimDash.RotateObject(_dashDirection);
            
            _isDashing = true;
            _dashTime = dashDuration;
        }
    }
}

