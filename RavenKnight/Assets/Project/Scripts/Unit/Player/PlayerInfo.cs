using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Target))]
public class PlayerInfo : MonoBehaviour
{
    public float Speed { get => _speed; }
    [SerializeField] private float _speed = 1;
    public DamageableTag DamageableTag { get => _damageableTag; }
    [SerializeField] private DamageableTag _damageableTag;
    public bool IsDead { get => _isDead; }
    [Space(10)][SerializeField] private bool _isDead;
    public List<MonoBehaviour> DisableComponents { get => _disableComponents; }
    [SerializeField] private List<MonoBehaviour> _disableComponents;
    public TargetInfo TargetInfo => _targetInfo;
    private TargetInfo _targetInfo;
    public Transform JointGun => _jointGun;
    [SerializeField] private Transform _jointGun;
    public ParticleSystem Particle => _particle;
    [SerializeField] private ParticleSystem _particle;
    
    public Weapon weapon;

    private void Awake()
    {
        _targetInfo = GetComponent<TargetInfo>();
        GlobalEvents.bossRoomClear += OnBossRoomClear;
    }

    private void Start()
    {
        GlobalEvents.SendPlayerInit(TargetInfo.Target);
    }

    private void OnDestroy()
    {
        GlobalEvents.bossRoomClear -= OnBossRoomClear;
    }

    private void OnBossRoomClear()
    {
        foreach (var comp in _disableComponents)
            comp.enabled = false;
    }
}
