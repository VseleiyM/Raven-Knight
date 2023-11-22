using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeAttackInfo : MonoBehaviour
{
    public Animator Animator => _animator;
    private Animator _animator;
    public AudioSource AudioSource => _audioSource;
    private AudioSource _audioSource;
    public Splash Splash => _splash;
    [SerializeField] private Splash _splash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
}
