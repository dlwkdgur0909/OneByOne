using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    private bool isRotated = false;

    private Vector3 originalRot = new Vector3(0, 0, 0);
    private Vector3 rotatedRot = new Vector3(0, 180, 0);

    private RectTransform rectTransform;

    public GameObject streetLightDes;
    public GameObject towerDes;
    public GameObject wireDes;
    public GameObject damageUpDes;
    public GameObject reloadSpeedDes;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
            {
                OnRotateButtonClick();
            }
        }
    }

    void OnRotateButtonClick()
    {
        if (!isRotated)
        {
            rectTransform.rotation = Quaternion.Euler(rotatedRot);
            isRotated = true;
        }
        else
        {
            rectTransform.rotation = Quaternion.Euler(originalRot);
            isRotated = false;
        }
    }
}
