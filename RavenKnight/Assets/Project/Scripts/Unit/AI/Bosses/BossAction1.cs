using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction1 : MonoBehaviour
{
    [SerializeField] private MobInfo mobInfo;
    [Header("Предсмертное разделение")]
    [SerializeField] private GameObject deathSpawn;
    [Min(0f)]
    [SerializeField] private float spawnRadius = 0f;
    [Min(1)]
    [SerializeField] private int count = 1;

    private Transform units;

    private void Awake()
    {
        var goUnits = GameObject.Find("Units");
        if (!goUnits)
            units = new GameObject("Units").transform;
        else
            units = goUnits.transform;
    }

    public void SoundEffect(string nameAudio)
    {
        foreach (var clip in mobInfo.ListAudioClip)
        {
            if (clip.name.ToLower() == nameAudio)
            {
                mobInfo.AudioSource.clip = clip;
                mobInfo.AudioSource.Play();
                break;
            }
        }
    }

    public void SendBossDead()
    {
        GlobalEvents.SendBossDead(mobInfo.Mob);
        GlobalEvents.SendScoreChanged(mobInfo.Mob.GainScore);
        GlobalEvents.SendCreateScoreText(mobInfo.transform.position, mobInfo.Mob.GainScore);
    }

    public void Dead()
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPointV2 = new Vector2(transform.position.x, transform.position.y);
            Vector2 offset = spawnPointV2 + Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPointV3 = new Vector3(offset.x, offset.y, 0f);
            var enemy = Instantiate(deathSpawn, spawnPointV3, Quaternion.identity);
            enemy.transform.parent = units;
            var mobInfo = enemy.GetComponent<MobInfo>();
            mobInfo.Animator.SetFloat("SpawnDelay", 6);
        }
        Destroy(mobInfo.gameObject);
    }

    public void MobSpawned()
    {
        mobInfo.PhysicsCollider.enabled = true;
        mobInfo.Mob.EnableAI();
    }
}
