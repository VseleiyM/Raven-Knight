using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobInfo : MonoBehaviour
{
    public NavMeshAgent Agent { get => _agent; }
    [SerializeField] private NavMeshAgent _agent;
    public Collider2D PhysicsCollider { get => _physicsCollider; }
    [SerializeField] private Collider2D _physicsCollider;
    public Collider2D AttackTrigger { get => _attackTrigger; }
    [SerializeField] private Collider2D _attackTrigger;
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; }
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public Animator Animator { get => _animator; }
    [SerializeField] private Animator _animator;
    public Mob Mob { get => _mob; }
    [SerializeField] private Mob _mob;

    [Space(10)]
    [SerializeField] private UnitCommand startStep;
    public TypeAttack TypeAttack { get => _typeAttack; }
    [SerializeField] private TypeAttack _typeAttack;
    public GameObject Projectile { get => _projectile; }
    [SerializeField] private GameObject _projectile;
    public Transform PointForProjectile { get => _pointForProjectile; }
    [SerializeField] private Transform _pointForProjectile;

    [Space(10)]
    public Transform target;
    public Collider2D targetCollider;

    private Coroutine enableAI;

    private void Start()
    {
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetCollider = target.GetComponent<Collider2D>();

        enableAI = StartCoroutine(EnableAI(startStep));
    }

    private IEnumerator EnableAI(UnitCommand firstStep)
    {
        firstStep.RequestData(this);
        firstStep.Execute();
        UnitCommand step = firstStep.NextStep;
        yield return new WaitForSeconds(0.1f);

        while (true)
        {
            step.RequestData(this);
            step.Execute();
            step = step.NextStep;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
