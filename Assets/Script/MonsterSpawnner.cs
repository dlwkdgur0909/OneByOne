using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefab;
    public GameObject rangeObj;
    BoxCollider boxCollider;

    void Awake()
    {
        boxCollider = rangeObj.GetComponent<BoxCollider>();
    }

    void Start()
    {
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonster()
    {
        while (true)
        {
            int randomPrefab = Random.Range(0, monsterPrefab.Length);

            Instantiate(monsterPrefab[randomPrefab], Return_RandomPosition(), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObj.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = boxCollider.bounds.size.x;
        float range_Z = boxCollider.bounds.size.z;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 RandomPostion = new Vector3(range_X, 0f, range_Z);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
}