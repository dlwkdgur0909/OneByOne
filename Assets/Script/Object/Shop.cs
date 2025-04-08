using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    public GameObject shop;
    public GameObject objectPanel;
    public GameObject gunPanel;

    public TMP_Text totalText;
    public TMP_Text totalText2;

    int i;
    private bool isBuyTower = false;
    public bool isBuyCannon = false;

    [Header("골드")]
    public int totalGold;
    public GameObject buyLog;
    public GameObject insufficientGoldLog;
    public GameObject buyTowerLog;

    [Header("오브젝트")]
    public GameObject[] streetLight = new GameObject[3];
    public GameObject tower;
    public GameObject cannon;
    public GameObject bullet;

    [Header("이미지")]
    public GameObject streetLightImage;
    public GameObject towerImage;
    public GameObject cannonImage;
    public GameObject damageUpImage;
    public GameObject reloadSpeedImage;

    [Header("버튼")]
    public GameObject streetLightButton;
    public GameObject towerButton;
    public GameObject cannonButton;
    public GameObject damageUpButton;
    public GameObject reloadSpeedButton;
    public GameObject frontDoorRepairButton;

    [Header("상품 구매 이미지")]
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
        if(Input.GetKeyDown(KeyCode.U))
        {
            totalGold += 100000;
        }
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
        AudioManager.instance.InsufficientGold.Play();
        insufficientGoldLog.SetActive(true);
        yield return new WaitForSeconds(2f);
        insufficientGoldLog.SetActive(false);
    }

    //상품을 구매했을 때 나오는 문구
    IEnumerator Buy()
    {
        AudioManager.instance.buy.Play();
        buyLog.SetActive(true);
        yield return new WaitForSeconds(2f);
        buyLog.SetActive(false);
    }

    //타워 안사고 대포부터 사려고 했을 때 나오는 문구
    IEnumerator PleaseBuyTower()
    {
        AudioManager.instance.InsufficientGold.Play();
        buyTowerLog.SetActive(true);
        yield return new WaitForSeconds(2f);
        buyTowerLog.SetActive(false);
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
            streetLightAllBuyText.SetActive(true);
            Color color = streetLightImage.GetComponent<Image>().color;
            color.a = 0.5f;
            streetLightImage.GetComponent<Image>().color = color;
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
            towerAllBuyText.SetActive(true);
            isBuyTower = true;

            //모두 구매했으면 버튼 비활성화 하고 이미지 불투명하게 하기
            Color color = towerImage.GetComponent<Image>().color;
            color.a = 0.5f;
            towerImage.GetComponent<Image>().color = color;
            towerButton.GetComponent<Button>().interactable = false;
        }
        else StartCoroutine(InsufficientGoldLog());
    }

    public void BuyCannon()
    {
        //타워 안사고 대포부터 사려고 했을 때 나오는 문구
        if (!isBuyTower) StartCoroutine(PleaseBuyTower());
        if(totalGold < 800) StartCoroutine(InsufficientGoldLog());

        if (totalGold >= 800 && isBuyTower == true)
        {
            StartCoroutine(Buy());
            totalGold -= 800;
            cannon.SetActive(true);
            isBuyCannon = true;
            cannonAllBuyText.SetActive(true);

            //모두 구매했으면 버튼 비활성화 하고 이미지 불투명하게 하기
            Color color = cannonImage.GetComponent<Image>().color;
            color.a = 0.5f;
            cannonImage.GetComponent<Image>().color = color;
            cannonButton.GetComponent<Button>().interactable = false;
        }
    }

    //공격력 상승
    public void BuyDamageUp()
    {
        if (totalGold >= 10)
        {
            StartCoroutine(Buy());
            totalGold -= 10;
            bullet.GetComponent<Bullet>().DMG += (int)0.5f;
            StartCoroutine(WaitForSeconds(damageUpButton));
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
            StartCoroutine(WaitForSeconds(reloadSpeedButton));
        }
        else StartCoroutine(InsufficientGoldLog());
    }

    //대문 수리
    public void FrontDoorRepair()
    {
        if (totalGold >= 30 && MainDoor.doorHP <= 99)
        {
            StartCoroutine(Buy());
            totalGold -= 30;
            MainDoor.doorHP = 100;
            StartCoroutine(WaitForSeconds(frontDoorRepairButton));
        }
        else StartCoroutine(InsufficientGoldLog());
    }
}
