using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnner : MonoBehaviour
{
    public GameObject monsterPrefab;

    void Start()
    {
        StartCoroutine(CO_MonsterSpawn());
    }

    void Update()
    {
       
    }

    public IEnumerator CO_MonsterSpawn()
    {
        Instantiate(monsterPrefab);
        yield return new WaitForSeconds(3f);
    }
}
