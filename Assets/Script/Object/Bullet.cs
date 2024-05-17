using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public int DMG = 0;
    public float speed;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();

            if (monster != null)
            {
                monster.Hp -= DMG;
                Destroy(collision.gameObject);
            }
        }
    }
}
