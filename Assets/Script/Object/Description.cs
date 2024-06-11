using UnityEngine;
using DG.Tweening;

//이건 추후 개발...
public class Description : MonoBehaviour
{
    private Vector3 originalRot = new Vector3(0, 0, 0);
    private Vector3 rotatedRot = new Vector3(0, 180, 0);

    public GameObject streetLightDes;
    public GameObject towerDes;
    public GameObject wireDes;
    public GameObject damageUpDes;
    public GameObject reloadSpeedDes;

    private RectTransform streetLightRect;
    private RectTransform towerRect;
    private RectTransform wireRect;
    private RectTransform damageUpRect;
    private RectTransform reloadSpeedRect;

    private bool isStreetLightRotated = false;
    private bool isTowerRotated = false;
    private bool isWireRotated = false;
    private bool isDamageUpRotated = false;
    private bool isReloadSpeedRotated = false;

    void Start()
    {
        streetLightRect = streetLightDes.GetComponent<RectTransform>();
        towerRect = towerDes.GetComponent<RectTransform>();
        wireRect = wireDes.GetComponent<RectTransform>();
        damageUpRect = damageUpDes.GetComponent<RectTransform>();
        reloadSpeedRect = reloadSpeedDes.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Input.mousePosition;
            Debug.Log($"Mouse position: {mousePosition}");

            if (RectTransformUtility.RectangleContainsScreenPoint(streetLightRect, mousePosition))
            {
                OnRotateButtonClick(streetLightRect, ref isStreetLightRotated);
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(towerRect, mousePosition))
            {
                OnRotateButtonClick(towerRect, ref isTowerRotated);
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(wireRect, mousePosition))
            {
                OnRotateButtonClick(wireRect, ref isWireRotated);
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(damageUpRect, mousePosition))
            {
                OnRotateButtonClick(damageUpRect, ref isDamageUpRotated);
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(reloadSpeedRect, mousePosition))
            {
                OnRotateButtonClick(reloadSpeedRect, ref isReloadSpeedRotated);
            }
        }
    }

    void OnRotateButtonClick(RectTransform rectTransform, ref bool isRotated)
    {
        if (!isRotated)
        {
            rectTransform.DORotate(rotatedRot,0.3f).SetEase(Ease.Linear);
            isRotated = true;
        }
        else
        {
            rectTransform.DORotate(originalRot, 0.3f).SetEase(Ease.Linear);
            isRotated = false;
        }
    }
}
