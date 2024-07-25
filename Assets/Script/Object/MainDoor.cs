using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public int doorHP = 0;

    public TMPro.TMP_Text HPText;

    private void Update()
    {
        HPText.text = "������:" + doorHP.ToString();

        if (doorHP <= 0)
        {
            //���� �ν������ϴ� ����
            GameManager.instance.frontDoor = null;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int Damege)
    {
        doorHP -= Damege;
    }
}
