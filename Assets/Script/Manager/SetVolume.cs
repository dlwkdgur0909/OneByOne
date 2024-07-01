using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void AudioControl(float sliderVal)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderVal) * 20);
    }
}
