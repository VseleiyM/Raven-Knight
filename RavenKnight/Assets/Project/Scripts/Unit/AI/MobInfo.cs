using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobInfo : MonoBehaviour
{
    public Collider2D PhysicsCollider => _physicsCollider;
    [SerializeField] private Collider2D _physicsCollider;
    public Collider2D AttackTrigger => _attackTrigger;
    [SerializeField] private Collider2D _attackTrigger;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public Animator Animator => _animator;
    private Animator _animator;
    public NavMeshAgent Agent => _agent;
    private NavMeshAgent _agent;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    private Rigidbody2D _rigidbody2D;
    public AudioSource AudioSource => _audioSource;
    private AudioSource _audioSource;
    public Mob Mob => _mob;
    private Mob _mob;
    public Vector2 Normal => _normal;
    private Vector2 _normal;

    public Transform PointForProjectile => _pointForProjectile;
    [SerializeField] private Transform _pointForProjectile;
    public List<Collider2D> Colliders2D => _colliders2D;
    [SerializeField] private List<Collider2D> _colliders2D;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _mob = GetComponent<Mob>();
        _animator = GetComponentInChildren<Animator>();

        Agent.updateUpAxis = false;
        Agent.updateRotation = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _normal = collision.contacts[collision.contacts.Length - 1].normal;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _normal = Vector2.zero;
    }

    public Vector2 Project(Vector2 direction)
    {

        if (Vector2.Dot(direction, _normal) > 0)
        {
            return direction;
        }
        else
        {
            return (direction - Vector2.Dot(direction, _normal) * _normal).normalized;
        }
    }
}
