using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public int doorHP = 0;

    private void Update()
    {
        if(doorHP <= 0)
        {
            //문이 부숴졌습니다 띄우기
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int Damege)
    {
        doorHP -= Damege;
    }
}
