using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    //TODO. 우클릭을 누르면 해당 오브젝트의 설명 나오게 하기
    //gunPanel부분 구현하기
    public static Shop instance;

    public GameObject shop;
    public GameObject objectPanel;
    public GameObject gunPanel;

    public TMP_Text totalText;
    public TMP_Text totalText2;

    [Header("골드")]
    public int totalGold;
    public GameObject buyLog;
    public GameObject insufficientGoldLog;

    [Header("오브젝트")]
    int i;
    public GameObject[] streetLight = new GameObject[3];
    public GameObject tower;
    public GameObject bullet;

    [Header("버튼")]
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
        totalText.text = "골드:" + totalGold.ToString();
        totalText2.text = "골드:" + totalGold.ToString();
    }

    //연타 방지
    private IEnumerator WaitForSeconds(GameObject Button)
    {
        Button.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(0.2f);
        Button.GetComponent<Button>().interactable = true;
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
        //가로등을 모두 구매했을 때
        if (i == 3)
        {
            streetLightButton = GameObject.Find("Shop Canvas/Object Panel/Buy Street Light Button");
            Color color = streetLightButton.GetComponent<Image>().color;
            color.a = 0.5f;
            streetLightButton.GetComponent<Image>().color = color;
            streetLightButton.GetComponent<Button>().interactable = false;
        }
    }

    //타워 구매
    public void BuyTower()
    {
        if (totalGold >= 500)
        {
            StartCoroutine(Buy());
            totalGold -= 500;
            tower.SetActive(true);

            //타워 버튼 투명하게하고 비활성화 하기
            towerButton.GetComponent<Button>().interactable = false;
            towerButton = GameObject.Find("Shop Canvas/Object Panel/Buy Cannon Button");
            Color color = towerButton.GetComponent<Image>().color;
            color.a = 0.5f;
            towerButton.GetComponent<Image>().color = color;
        }
        else StartCoroutine(InsufficientGoldLog());
    }

    //철조망 구매

    //공격력 상승
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

    //재장전 속도 상승
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
