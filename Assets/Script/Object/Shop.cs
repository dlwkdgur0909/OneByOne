using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    //TODO. ��Ŭ���� ������ �ش� ������Ʈ�� ���� ������ �ϱ�
    //gunPanel�κ� �����ϱ�
    public static Shop instance;

    public GameObject shop;
    public GameObject objectPanel;
    public GameObject gunPanel;

    public TMP_Text totalText;
    public TMP_Text totalText2;

    [Header("���")]
    public int totalGold;
    public GameObject buyLog;
    public GameObject insufficientGoldLog;

    [Header("������Ʈ")]
    int i;
    public GameObject[] streetLight = new GameObject[3];
    public GameObject tower;
    public GameObject bullet;

    [Header("��ư")]
    public GameObject streetLightButton;
    public GameObject towerButton;
    public GameObject DamageUpButton;
    public GameObject ReLoadSpeedButton;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        bullet.GetComponent<Bullet>().DMG = 1;
    }

    void Update()
    {
        TotalGold();
    }

    public void AddGold(int goldValue)
    {
        totalGold += goldValue;
    }

    public void TotalGold()
    {
        totalText.text = "���:" + totalGold.ToString();
        totalText2.text = "���:" + totalGold.ToString();
    }

    //��Ÿ ����
    private IEnumerator WaitForSeconds(GameObject Button)
    {
        Button.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(0.2f);
        Button.GetComponent<Button>().interactable = true;
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
        if (totalGold >= 50)
        {
            StartCoroutine(Buy());
            if (i <= streetLight.Length)
            {
                totalGold -= 50;
                streetLight[i].SetActive(true);
                i++;
            }
        }
        else StartCoroutine(InsufficientGoldLog());
        //���ε��� ��� �������� ��
        if (i == 3)
        {
            streetLightButton = GameObject.Find("Shop Canvas/Object Panel/Buy Street Light Button");
            Color color = streetLightButton.GetComponent<Image>().color;
            color.a = 0.5f;
            streetLightButton.GetComponent<Image>().color = color;
            streetLightButton.GetComponent<Button>().interactable = false;
        }
    }

    //Ÿ�� ����
    public void BuyTower()
    {
        if (totalGold >= 500)
        {
            StartCoroutine(Buy());
            totalGold -= 500;
            tower.SetActive(true);

            //Ÿ�� ��ư �����ϰ��ϰ� ��Ȱ��ȭ �ϱ�
            towerButton.GetComponent<Button>().interactable = false;
            towerButton = GameObject.Find("Shop Canvas/Object Panel/Buy Cannon Button");
            Color color = towerButton.GetComponent<Image>().color;
            color.a = 0.5f;
            towerButton.GetComponent<Image>().color = color;
        }
        else StartCoroutine(InsufficientGoldLog());
    }

    //ö���� ����

    //���ݷ� ���
    public void BuyDamageUp()
    {
        if (totalGold >= 10)
        {
            StartCoroutine(Buy());
            totalGold -= 10;
            bullet.GetComponent<Bullet>().DMG++;
            StartCoroutine(WaitForSeconds(DamageUpButton));
        }
        else StartCoroutine(InsufficientGoldLog());
    }

    //������ �ӵ� ���
    public void BuyReLoadSpeed()
    {
        if (totalGold >= 15)
        {
            StartCoroutine(Buy());
            totalGold -= 15;
            mainCamera.instance.reloadTime -= 0.05f;
            StartCoroutine(WaitForSeconds(ReLoadSpeedButton));
        }
        else StartCoroutine(InsufficientGoldLog());
    }
}
