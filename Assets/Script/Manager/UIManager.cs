using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject howToPlay;
    public GameObject setting;
    public GameObject credit;
    public GameObject exit;
    
    public void Titie()
    {
        SceneManager.LoadScene(0);
    }

    public void InGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void On_HowToPlay()
    {
        howToPlay.SetActive(true);
    }

    public void Off_HowToPlay()
    {
        howToPlay.SetActive(false);
    }

    public void On_Setting()
    {
        setting.SetActive(true);
    }

    public void Off_Setting()
    {
        setting.SetActive(false);
    }

    public void On_Credit()
    {
        credit.SetActive(true);
    }

    public void Off_Credit()
    {
        credit.SetActive(false);
    }

    public void On_Exit()
    {
        exit.SetActive(true);
    }

    public void Off_Exit()
    {
        exit.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
