using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiticBombshell : UnitCommand
{
    public override UnitCommand NextStep => _nextStep;
    [SerializeField] private UnitCommand _nextStep;

    [Min(0)]
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private BoxCollider2D areaAttack;
    [Min(0)]
    [SerializeField] private float deadZoneBox;
    [SerializeField] private GameObject areaEffectPrefab;
    [Min(1)]
    [SerializeField] private int count = 1;
    [SerializeField] private DamageableTag damageableTag;

    private bool isReady = true;
    private Transform temp;

    private void Awake()
    {
        var goTemp = GameObject.Find("Temp");
        if (!goTemp)
            temp = new GameObject("Temp").transform;
        else
            temp = goTemp.transform;
    }

    public override void RequestData(MobInfo mobInfo)
    {
        
    }

    public override void Execute()
    {
        if (!isReady) return;

        for (int i = 0; i < count; i++)
        {
            float minX = areaAttack.size.x * -0.5f;
            float maxX = areaAttack.size.x * 0.5f;
            float minY = areaAttack.size.y * -0.5f;
            float maxY = areaAttack.size.y * 0.5f;

            float spawnX = transform.position.x + Random.Range(minX, maxX);
            float spawnY = transform.position.y + Random.Range(minY, maxY);

            bool pointIn = true;
            while (pointIn)
            {
                bool checkIn = 
                    spawnX < (transform.position.x + deadZoneBox) &&
                    spawnX > (transform.position.x - deadZoneBox) &&
                    spawnY < (transform.position.y + deadZoneBox) &&
                    spawnY > (transform.position.y - deadZoneBox);

                if (checkIn)
                {
                    spawnX = transform.position.x + Random.Range(minX, maxX);
                    spawnY = transform.position.y + Random.Range(minY, maxY);
                }
                else
                    pointIn = false;
            }

            Vector3 spawnPoint = new Vector3(spawnX, spawnY, 0f);

            var area = Instantiate(areaEffectPrefab, spawnPoint, Quaternion.identity);
            area.transform.parent = temp;
            var compAreaEffect = area.GetComponent<AreaEffect>();
            compAreaEffect.gameObject.layer = gameObject.layer;
            compAreaEffect.damageableTag = damageableTag;
        }

        isReady = false;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }
}
