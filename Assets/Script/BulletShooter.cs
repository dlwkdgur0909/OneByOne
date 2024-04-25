using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public GameObject bullet;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }
}
