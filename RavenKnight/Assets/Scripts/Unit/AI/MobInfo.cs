using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Project
{
    [RequireComponent(typeof(Target))]
    public class MobInfo : MonoBehaviour
    {
        public TargetInfo TargetInfo => _targetInfo;
        private TargetInfo _targetInfo;
        public NavMeshAgent Agent => _agent;
        private NavMeshAgent _agent;
        public MobAction MobAction => _mobAction;
        private MobAction _mobAction;
        public Collider2D AttackTrigger => _attackTrigger;
        [SerializeField] private Collider2D _attackTrigger;
        [Space(10)]
        [SerializeField] private List<UnitCommand> startStep;
        [Space(10)]
        [HideInInspector] public Transform target;
        [HideInInspector] public Collider2D targetCollider;
        [SerializeField] private bool isBoss = false;
        private bool enableAI = false;
        public Coroutine corotine_AI;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _targetInfo = GetComponent<TargetInfo>();
            _mobAction = GetComponentInChildren<MobAction>();

            if (Agent != null)
            {
                Agent.updateUpAxis = false;
                Agent.updateRotation = false;
            }

            GlobalEvents.SendMobSpawned(TargetInfo.Target);
        }

        private void Start()
        {
            if (GameObject.FindGameObjectWithTag(GameObjectTag.Player.ToString()))
            {
                target = GameObject.FindGameObjectWithTag(GameObjectTag.Player.ToString()).transform;
                targetCollider = target.GetComponent<Collider2D>();
            }
            GlobalEvents.playerDead += OnPlayerDead;

            if (TargetInfo.Target.ReturnParameter(ParametersList.IsBoss) != null)
                GlobalEvents.SendBossInit(this.TargetInfo.Target);
        }
        private void OnDestroy()
        {
            GlobalEvents.playerDead -= OnPlayerDead;
        }

        private void FixedUpdate()
        {
            if (!enableAI) return;

            for (int i = 0; i < startStep.Count; i++)
            {
                startStep[i].RequestData(this);
                startStep[i].Execute();
                startStep[i] = startStep[i].NextStep;
            }
        }

        public void EnableAI()
        {
            enableAI = true;
            Agent.isStopped = false;

            foreach (var component in TargetInfo.Colliders2D)
                component.enabled = true;
        }

        public void OnPlayerDead(Target player)
        {
            enableAI = false;
            TargetInfo.Animator.SetBool(AnimatorParameter.Run.ToString(), false);
            TargetInfo.Animator.SetBool(AnimatorParameter.Attack.ToString(), false);
            if (Agent.isActiveAndEnabled)
                Agent.SetDestination(this.transform.position);
        }

        public void TakeDamage()
        {
            if (isBoss)
            {
                GlobalEvents.SendBossTakeDamage(TargetInfo.Target);
            }
        }
    }
}