using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    // 할 것 : 스폰 영역 구하기
    public GameObject[] monsterPrefab;
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
        int randomPrefab = Random.Range(0, monsterPrefab.Length);

        Instantiate(monsterPrefab[randomPrefab], Return_RandomPosition(), Quaternion.identity);
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
        Vector3 originPosition = rangeObj.transform.position;
        Vector3 obstaclePosition = obstacleObj.transform.position;
        float range_X = boxCollider.bounds.size.x;
        float range_Z = boxCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, -3f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion - obstaclePosition;
        return respawnPosition;
    }
}
