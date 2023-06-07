using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeAttackInfo : MonoBehaviour
{
    public Animator Animator { get => _animator; }
    [SerializeField] private Animator _animator;
    public AudioSource AudioSource { get => _audioSource; }
    [SerializeField] private AudioSource _audioSource;
    public List<AudioClip> ListAudioClip { get => _listAudioClip; }
    [Space(10)]
    [SerializeField] private List<AudioClip> _listAudioClip;
}
