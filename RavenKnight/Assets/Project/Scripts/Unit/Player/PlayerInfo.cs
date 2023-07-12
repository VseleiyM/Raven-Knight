﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public Animator Animator { get => _animator; }
    [SerializeField] private Animator _animator;
    public AudioSource AudioSource { get => _audioSource; }
    [SerializeField] private AudioSource _audioSource;
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; }
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public Transform JointGun { get => _jointGun; }
    [SerializeField] private Transform _jointGun;
    public Player Player { get => _player; }
    [SerializeField] private Player _player;

    public Animator ChildAnimator { get => _childAnimator; }
    [SerializeField] private Animator _childAnimator;

    public Weapon weapon;
}
