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
            //���� �ν������ϴ� ����
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int Damege)
    {
        doorHP -= Damege;
    }
}
