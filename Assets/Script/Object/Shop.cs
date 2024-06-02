using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //TODO. 우클릭을 누르면 해당 오브젝트의 설명 나오게 하기
    //gunPanel부분 구현하기
    public static Shop instance;

    public GameObject shop;
    public GameObject objectPanel;
    public GameObject gunPanel;

    [Header("골드")]
    public int totalGold;
    public GameObject buyLog;
    public GameObject insufficientGoldLog;

    [Header("오브젝트")]
    int i;
    public GameObject[] streetLight = new GameObject[3];
    public GameObject tower;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AddGold(int goldValue)
    {
        totalGold += goldValue;
    }

    //골드가 부족할 때 나오는 문구
    IEnumerator InsufficientGoldLog()
    {
        insufficientGoldLog.SetActive(true);
        yield return new WaitForSeconds(2f);
        insufficientGoldLog.SetActive(false);
    }

    //상품을 구매했을 때 나오는 문구
    IEnumerator Buy()
    {
        buyLog.SetActive(true);
        yield return new WaitForSeconds(2f);
        buyLog.SetActive(false);
    }

    public void OpenShop()
    {
        shop.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseShop()
    {
        shop.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera.instance.isShop = false;
    }

    public void OpenObjectPanel()
    {
        objectPanel.SetActive(true);
        gunPanel.SetActive(false);
    }

    public void OpenGunPanel()
    {
        objectPanel.SetActive(false);
        gunPanel.SetActive(true);
    }

    //가로등 구매
    public void BuyStreetLight()
    {
        if (totalGold >= 100)
        {
            StartCoroutine(Buy());
            if (i <= streetLight.Length)
            {
                totalGold = totalGold - 100;
                streetLight[i].SetActive(true);
                i++;
            }
        }
        else StartCoroutine(InsufficientGoldLog());
    }

    //타워 구매
    public void BuyTower()
    {
        if (totalGold >= 1000)
        {
            StartCoroutine(Buy());
            totalGold = totalGold - 1000;
            tower.SetActive(true);
        }
        else StartCoroutine(InsufficientGoldLog());
    }

    //철조망 구매

    //공격력 상승
    public void BuyDamageUp()
    {
        if(totalGold >= 10)
        {
            StartCoroutine(Buy());
            Bullet bullet = GetComponent<Bullet>();
            bullet.DMG += 1;
        }
        else StartCoroutine(InsufficientGoldLog());
    }

    //재장전 속도 상승
    public void BuyReLoadSpeed()
    {
        if( totalGold >= 15)
        {
            StartCoroutine(Buy());
            mainCamera.instance.reloadTime -= 0.1f;
        }
        else StartCoroutine(InsufficientGoldLog());
    }
}
