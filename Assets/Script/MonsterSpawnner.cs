using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnner : MonoBehaviour
{
    public GameObject monsterPrefab;


    void Update()
    {
        CO_MonsterSpawn();
    }

    IEnumerator CO_MonsterSpawn()
    {
        Instantiate(monsterPrefab);
        yield return new WaitForSeconds(3f);
        Destroy(monsterPrefab);
    }
}
