using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Description : MonoBehaviour
{
    public enum DescriptionImage
    {  
        streetLight,
        tower,
        damageUp,
        reloadSpeed
    }
         
    [Header("상품 설명 이미지")]
    public GameObject streetLightDescriptionImage;
    public GameObject towerDescriptionImage;
    public GameObject damageUpDescriptionImage;
    public GameObject reloadSpeedDescriptionImage;

    [SerializeField] public DescriptionImage curImage = DescriptionImage.streetLight;
    public DescriptionImage CurImage { get { return curImage; } set { curImage = value; OnMouseDown(); } }

    private void OnMouseDown()
    {
        switch(curImage)
        {
            case DescriptionImage.streetLight:
                streetLightDescriptionImage.SetActive(true);
                break;
        }
    }

    //private IEnumerator SetActiveDescriptionImage(DescriptionImage index)
    //{

    //}
}

