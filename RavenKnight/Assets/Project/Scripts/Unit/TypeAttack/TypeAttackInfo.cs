using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeAttackInfo : MonoBehaviour
{
    public Animator Animator { get => _animator; }
    [SerializeField] private Animator _animator;
    public AudioSource AudioSource { get => _audioSource; }
    [SerializeField] private AudioSource _audioSource;
}
