using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawnner : MonoBehaviour
{
    public GameObject[] monsterPrefab; // 몬스터 프리팹 배열
    public Vector3 fieldSize; // 전체 필드 크기
    public Vector3 spawnSize; // 스폰 영역 크기
    public float noSpawnRegionSize = 100f;

    void Start()
    {
        fieldSize = new Vector3(500, 0, 500);
        spawnSize = new Vector3(300, 0, 300);

        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            Vector3 randomPosition = GetRandomPositionInSpawnArea();

            int randomMonsterIndex = Random.Range(0, monsterPrefab.Length);
            GameObject newMonster = Instantiate(monsterPrefab[randomMonsterIndex], randomPosition, Quaternion.identity);
            // 몬스터의 높이 고려하여 위치 조정
            newMonster.transform.position += Vector3.up * newMonster.GetComponent<Collider>().bounds.extents.y;

            yield return new WaitForSeconds(3f);
        }
    }

    Vector3 GetRandomPositionInSpawnArea()
    {
        Vector3 center = transform.position;
        float halfWigth = spawnSize.x / 2f;
        float halfDepth = spawnSize.z / 2f;

        Vector3 spawnCenter = center;
        spawnCenter.y = transform.position.y;

        Vector3 noSpawnRegion = new Vector3(noSpawnRegionSize, 0, noSpawnRegionSize);


        Vector3 spawnAreaMin = spawnCenter - spawnSize / 2f + noSpawnRegion / 2f;
        Vector3 spawnAreaMax = spawnCenter + spawnSize / 2f - noSpawnRegion / 2f;

        Vector3 randomPosition = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            transform.position.y,
            Random.Range(spawnAreaMin.z, spawnAreaMax.z)
        );
        return randomPosition;
    }
}
