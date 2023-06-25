using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobInfo : MonoBehaviour
{
    public NavMeshAgent Agent => _agent;
    [SerializeField] private NavMeshAgent _agent;
    public Collider2D PhysicsCollider => _physicsCollider;
    [SerializeField] private Collider2D _physicsCollider;
    public Collider2D AttackTrigger => _attackTrigger;
    [SerializeField] private Collider2D _attackTrigger;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public Animator Animator => _animator;
    [SerializeField] private Animator _animator;
    public AudioSource AudioSource => _audioSource;
    [SerializeField] private AudioSource _audioSource;
    public Mob Mob => _mob;
    [SerializeField] private Mob _mob;

    public AIEnumTypeAttack TypeAttack => _typeAttack;
    [Space(10)]
    [SerializeField] private AIEnumTypeAttack _typeAttack;
    public GameObject Projectile => _projectile;
    [SerializeField] private GameObject _projectile;
    public Transform PointForProjectile => _pointForProjectile;
    [SerializeField] private Transform _pointForProjectile;

    private void Awake()
    {
        Agent.updateUpAxis = false;
        Agent.updateRotation = false;
    }
}
