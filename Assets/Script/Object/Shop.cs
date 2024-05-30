using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static mainCamera;

public class Shop : MonoBehaviour
{
    public GameObject shop;
    public GameObject[] streetLight;

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
        instance.isShop = false;    
    }

    public void StreetLight()
    {
        //구매할 때마다 순서대로 가로등 생성
        //streetLight[].SetActive(true);
    }
}
