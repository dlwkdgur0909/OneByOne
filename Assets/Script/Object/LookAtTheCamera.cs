using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTheCamera : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(GameManager.instance.cameraTrans.position);
    }
}
