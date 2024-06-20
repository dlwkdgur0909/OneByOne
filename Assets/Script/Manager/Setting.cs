using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] private GameObject soundPanel;
    [SerializeField] private GameObject playPanel;

    [SerializeField] private Slider rotSpeedSlider;
    [SerializeField] private GameObject getRotSpeed;
    private float rotSpeed;
    private RotateCamera rotateCamera;
    //¹à±â Á¶Àý

    private void Start()
    {
        rotateCamera = getRotSpeed.GetComponent<RotateCamera>();
        rotSpeedSlider.value = rotateCamera.rotSpeed;
        rotSpeedSlider.onValueChanged.AddListener(SliderValueChange);
    }

    private void SliderValueChange(float value)
    {
        if(rotateCamera != null) rotateCamera.rotSpeed = value;
    }
}
