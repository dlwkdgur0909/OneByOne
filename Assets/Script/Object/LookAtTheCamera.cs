using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTheCamera : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.LookAt(target.transform.position);
    }
}
