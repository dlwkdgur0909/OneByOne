using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;
<<<<<<< HEAD
=======
using DG.Tweening;
>>>>>>> 192c12c (Game Clear and Game over 2)

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public Transform door;
    public Transform frontDoor;
    public Transform cameraTrans;
    public Transform playerPos;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

