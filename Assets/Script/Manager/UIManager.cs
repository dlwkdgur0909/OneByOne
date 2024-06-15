using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public float moveSpeed;
    public GameObject blackPanel;
    public GameObject start;
    public GameObject howToPlay;
    public GameObject setting;
    public GameObject credit;
    public GameObject exit;

    #region Button
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
        Debug.Log("Game Quit");
        Application.Quit();
    }
    #endregion

    private void Update()
    {
        //게임 시작할때 샤라락 ㅇㅋ?
        blackPanel.transform.Translate(new Vector3(-100f, 0, 0) * moveSpeed * Time.deltaTime); 
    }
}
