using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefab;
    public float[] monsterSpawnProbability; // 확률 값 배열
    public GameObject rangeObj;
    public GameObject obstacleObj;
    BoxCollider boxCollider;
    public float spawnMonsterTime;

    void Awake()
    {
        boxCollider = rangeObj.GetComponent<BoxCollider>();
    }

    void Start()
    {
        StartCoroutine(SpawnMonster(spawnMonsterTime));
    }

    void SpawnRandomMonster()
    {
        int randomIndex = ChooseMonsterIndex();
        Instantiate(monsterPrefab[randomIndex], Return_RandomPosition(), Quaternion.identity);
    }

    int ChooseMonsterIndex()
    {
        float totalProbability = 0f;
        foreach (float probability in monsterSpawnProbability)
        {
            totalProbability += probability;
        }

        float randomValue = Random.Range(0f, totalProbability);
        float cumulativeProbability = 0f;
        for (int i = 0; i < monsterSpawnProbability.Length; i++)
        {
            cumulativeProbability += monsterSpawnProbability[i];
            if (randomValue <= cumulativeProbability)
            {
                return i;
            }
        }

        // 이 지점에 도달할 경우 문제가 있으므로 기본값을 반환합니다.
        return 0;
    }

    IEnumerator SpawnMonster(float time)
    {
        while (true)
        {
            SpawnRandomMonster();
            yield return new WaitForSeconds(time);
        }
    }

    Vector3 Return_RandomPosition()
    {
        float range_X = boxCollider.bounds.size.x;
        float range_Z = boxCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, -3f, range_Z);

        return RandomPostion;
    }
}
