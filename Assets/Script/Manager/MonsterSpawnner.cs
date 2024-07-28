using System.Collections;
using UnityEngine;

public class MonsterSpawnner : MonoBehaviour
{
    public GameObject[] monsterPrefab;
    public GameObject rangeObj;
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
        int randomIndex = Random.Range(0, monsterPrefab.Length);
        Instantiate(monsterPrefab[randomIndex], Return_RandomPosition(), Quaternion.identity);
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
