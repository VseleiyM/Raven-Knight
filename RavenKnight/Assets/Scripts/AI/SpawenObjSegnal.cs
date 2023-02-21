using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawenObjSegnal : MonoBehaviour
{
    public GameObject spawnedRoundMob;
    private SpawenEneme Spawn;
    private Vector3 Posiceon;

    void Start()
    {
        Posiceon = gameObject.transform.position;
        Invoke("SpawenObject", 3);    
    }

    void SpawenObject()
    {
        Spawn = GameObject.FindGameObjectWithTag("Spawen").GetComponent<SpawenEneme>();
        GameObject spawnedRound = Instantiate(spawnedRoundMob, Posiceon, transform.rotation);
        Spawn.zombie.Add(spawnedRound);
        Spawn.zombie.Remove(gameObject);
        Destroy(gameObject);
    }
}
