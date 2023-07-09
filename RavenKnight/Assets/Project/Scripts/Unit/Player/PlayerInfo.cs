using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public Animator Animator => _animator;
    [SerializeField] private Animator _animator;
    public AudioSource AudioSource => _audioSource;
    [SerializeField] private AudioSource _audioSource;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public Transform JointGun => _jointGun;
    [SerializeField] private Transform _jointGun;
    public Player Player => _player;
    [SerializeField] private Player _player;

    public Weapon weapon;
}
