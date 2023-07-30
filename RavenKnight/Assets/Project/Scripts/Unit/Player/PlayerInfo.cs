using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public Animator Animator => _animator;
    [SerializeField] private Animator _animator;
    public AudioSource AudioSource => _audioSource;
    private AudioSource _audioSource;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public Transform JointGun => _jointGun;
    [SerializeField] private Transform _jointGun;
    public Player Player => _player;
    private Player _player;
    public ParticleSystem Particle => _particle;
    [SerializeField] private ParticleSystem _particle;
    public Collider2D PhysycCollider => _physicCollider;
    private Collider2D _physicCollider;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    private Rigidbody2D _rigidbody2D;
    
    public Weapon weapon;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _player = GetComponent<Player>();
        _physicCollider = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
