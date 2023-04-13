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
    public AudioSource AudioSource { get => _audioSource; }
    [SerializeField] private AudioSource _audioSource;
    public Mob Mob { get => _mob; }
    [SerializeField] private Mob _mob;

    public TypeAttack TypeAttack { get => _typeAttack; }
    [Space(10)]
    [SerializeField] private TypeAttack _typeAttack;
    public GameObject Projectile { get => _projectile; }
    [SerializeField] private GameObject _projectile;
    public Transform PointForProjectile { get => _pointForProjectile; }
    [SerializeField] private Transform _pointForProjectile;
    public List<AudioClip> ListAudioClip { get => _listAudioClip; }
    [Space(10)]
    [SerializeField] private List<AudioClip> _listAudioClip;

    private void Awake()
    {
        Agent.updateUpAxis = false;
        Agent.updateRotation = false;
    }
}
