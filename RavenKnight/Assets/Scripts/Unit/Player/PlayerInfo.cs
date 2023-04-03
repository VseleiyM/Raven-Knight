using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public Animator Animator { get => _animator; }
    [SerializeField] private Animator _animator;
}
