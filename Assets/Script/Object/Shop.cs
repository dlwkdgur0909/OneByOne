using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //TODO. ��Ŭ���� ������ �ش� ������Ʈ�� ���� ������ �ϱ�
    //gunPanel�κ� �����ϱ�
    public static Shop instance;

    public GameObject shop;
    public GameObject objectPanel;
    public GameObject gunPanel;

    [Header("���")]
    public int totalGold;
    public GameObject buyLog;
    public GameObject insufficientGoldLog;

    [Header("������Ʈ")]
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

    //��尡 ������ �� ������ ����
    IEnumerator InsufficientGoldLog()
    {
        insufficientGoldLog.SetActive(true);
        yield return new WaitForSeconds(2f);
        insufficientGoldLog.SetActive(false);
    }

    //��ǰ�� �������� �� ������ ����
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

    //���ε� ����
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

    //Ÿ�� ����
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

    //ö���� ����

    //���ݷ� ���
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

    //������ �ӵ� ���
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
