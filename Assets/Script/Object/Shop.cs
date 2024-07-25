using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public GameObject shop;
    public GameObject objectPanel;
    public GameObject gunPanel;

    public TMP_Text totalText;
    public TMP_Text totalText2;

    int i;
    public bool isBuyCannon = false;

    [Header("���")]
    public int totalGold;
    public GameObject buyLog;
    public GameObject insufficientGoldLog;

    [Header("������Ʈ")]
    public GameObject[] streetLight = new GameObject[3];
    public GameObject tower;
    public GameObject cannon;
    public GameObject bullet;

    [Header("�̹���")]
    public GameObject streetLightImage;
    public GameObject towerImage;
    public GameObject cannonImage;
    public GameObject damageUpImage;
    public GameObject reloadSpeedImage;

    [Header("��ư")]
    public GameObject streetLightButton;
    public GameObject towerButton;
    public GameObject cannonButton;
    public GameObject damageUpButton;
    public GameObject reloadSpeedButton;

    [Header("��ǰ ���� �̹���")]
    public GameObject streetLightAllBuyText;
    public GameObject towerAllBuyText;
    public GameObject cannonAllBuyText;

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
        AudioManager.instance.InsufficientGold.Play();
        insufficientGoldLog.SetActive(true);
        yield return new WaitForSeconds(2f);
        insufficientGoldLog.SetActive(false);
    }

    //��ǰ�� �������� �� ������ ����
    IEnumerator Buy()
    {
        AudioManager.instance.buy.Play();
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
            streetLightAllBuyText.SetActive(true);
            Color color = streetLightImage.GetComponent<Image>().color;
            color.a = 0.5f;
            streetLightImage.GetComponent<Image>().color = color;
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
            towerAllBuyText.SetActive(true);

            //��� ���������� ��ư ��Ȱ��ȭ �ϰ� �̹��� �������ϰ� �ϱ�
            Color color = towerImage.GetComponent<Image>().color;
            color.a = 0.5f;
            towerImage.GetComponent<Image>().color = color;
        }
        else StartCoroutine(InsufficientGoldLog());
    }

    public void BuyCannon()
    {
        if (totalGold >= 800)
        {
            StartCoroutine(Buy());
            totalGold -= 800;
            cannon.SetActive(true);
            isBuyCannon = true;
            cannonAllBuyText.SetActive(true);

            //��� ���������� ��ư ��Ȱ��ȭ �ϰ� �̹��� �������ϰ� �ϱ�
            Color color = cannonImage.GetComponent<Image>().color;
            color.a = 0.5f;
            cannonImage.GetComponent<Image>().color = color;
        }
        else StartCoroutine(InsufficientGoldLog());
    }

    //���ݷ� ���
    public void BuyDamageUp()
    {
        if (totalGold >= 10)
        {
            StartCoroutine(Buy());
            totalGold -= 10;
            bullet.GetComponent<Bullet>().DMG++;
            StartCoroutine(WaitForSeconds(damageUpButton));
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
            StartCoroutine(WaitForSeconds(reloadSpeedButton));
        }
        else StartCoroutine(InsufficientGoldLog());
    }
}
