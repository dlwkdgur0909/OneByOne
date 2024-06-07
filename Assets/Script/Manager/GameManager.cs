using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public Transform door;
    public Transform frontDoor;

    //public GameObject settingPanel;
    //private bool IsPause = false;

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

    private void Update()
    {
        //SettingPanel°ü¸®
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (!IsPause)
        //    {
        //        settingPanel.SetActive(true);
        //        IsPause = true;
        //        Time.timeScale = 0;
        //    }
        //    else
        //    {
        //        settingPanel.SetActive(false);
        //        IsPause = false;
        //        Time.timeScale = 1.0f;
        //    }
        //}
    }
}

