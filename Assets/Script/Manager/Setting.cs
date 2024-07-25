using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Setting : MonoBehaviour
{
    #region Sound
    [Header("Sound")]
    [SerializeField] private GameObject soundPanel;
    #endregion

    #region Play
    [Header("PlayPanel")]
    //����
    [SerializeField] private GameObject playPanel;
    [SerializeField] private Slider rotSpeedSlider;
    [SerializeField] private GameObject getRotSpeed;

    //��� ����
    [SerializeField] private Slider brightnessSlider;
    #endregion

    private void Start()
    {
        rotSpeedSlider.value = RotateCamera.rotSpeed;
        rotSpeedSlider.onValueChanged.AddListener(SliderValueChange);

        brightnessSlider.value = RenderSettings.ambientIntensity;
        brightnessSlider.onValueChanged.AddListener(ChangeBrightness);
    }

    private void SliderValueChange(float value)
    {
        RotateCamera.rotSpeed = value;
    }

    private void ChangeBrightness(float value)
    {
        RenderSettings.ambientIntensity = value;
    }

}
