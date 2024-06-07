using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public int doorHP = 0;

    private void Update()
    {
        if(doorHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int Damege)
    {
        doorHP -= Damege;
    }
}
