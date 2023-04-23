using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawenBonus : MonoBehaviour
{
    public GameObject spawnedRoundMob;
    private Vector3 Posiceon;

    public void SPOJ()
    {
        GameObject spawnedRound = Instantiate(spawnedRoundMob, transform.position, transform.rotation);
        /*
        float innn = Random.Range(0f, 2f);
        if (innn == 0)
        {

        }*/
    }
}
