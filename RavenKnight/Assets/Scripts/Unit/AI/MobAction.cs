using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class MobAction : MonoBehaviour
    {
        private MobInfo mobInfo;
        private Transform folder;

        public event Action takeDamage;
        public event Action<int> attack;
        public event Action<int> attackFinished;

        private void Awake()
        {
            var goFolder = GameObject.Find("Temp");
            if (!goFolder)
                folder = new GameObject("Temp").transform;
            else
                folder = goFolder.transform;

            mobInfo = GetComponentInParent<MobInfo>();
        }

        public void SendMobDead()
        {
            GlobalEvents.SendMobDead(mobInfo.TargetInfo.Target);
            UnitParameter scoreParam = mobInfo.TargetInfo.Target.ReturnParameter(ParametersList.GainScore);
            if (scoreParam != null)
            {
                GlobalEvents.SendScoreChanged((int)scoreParam.Max);
                GlobalEvents.SendCreateScoreText(mobInfo.transform.position, (int)scoreParam.Max);
            }
            foreach (var component in mobInfo.TargetInfo.Colliders2D)
                component.enabled = false;
        }

        public void DestroyUnit()
        {
            Destroy(mobInfo.gameObject);
        }

        public void SoundEffect(AudioClip clip)
        {
            if (clip == null) return;

            mobInfo.TargetInfo.AudioSource.clip = clip;
            mobInfo.TargetInfo.AudioSource.Play();
        }

        public void SendTakeDamage()
        {
            takeDamage?.Invoke();
        }

        public void AttackVariant(int variant)
        {
            attack?.Invoke(variant);
        }

        public void AttackFinished(int variant)
        {
            attackFinished?.Invoke(variant);
        }

        public void MobSpawned()
        {
            mobInfo.TargetInfo.PhysicsCollider.enabled = true;
            mobInfo.EnableAI();
        }
    }
}