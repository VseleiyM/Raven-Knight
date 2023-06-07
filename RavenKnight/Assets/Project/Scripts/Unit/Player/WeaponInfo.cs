using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public Animator Animator { get => _animator; }
    [SerializeField] private Animator _animator;
    public AudioSource AudioSource { get => _audioSource; }
    [SerializeField] private AudioSource _audioSource;
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; }
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public List<AudioClip> ListAudioClip { get => _listAudioClip; }
    [Space(10)]
    [SerializeField] private List<AudioClip> _listAudioClip;
}
