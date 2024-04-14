using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnner : MonoBehaviour
{
    public GameObject[] monsterPrefab;
    private Vector3 exculusionVector;


    void Start()
    {
        Vector3 fieldSize = new Vector3(500, 0, 500);
        Vector3 rectangleSize = new Vector3(80, 0, 80);

        Vector3 rectangleCenter = fieldSize / 2;
        Vector3 rectangleTopLeft = rectangleCenter - rectangleSize / 2;

        exculusionVector = new Vector3(rectangleTopLeft.x, 0, rectangleTopLeft.z);

        StartCoroutine(CO_MonsterSpawn());
    }

    void Update()
    {

    }

    public IEnumerator CO_MonsterSpawn()
    {
        while (true)
        {
            int randomMonster = Random.Range(0, monsterPrefab.Length);
            Instantiate(monsterPrefab[randomMonster], exculusionVector, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }
}
