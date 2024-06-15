using UnityEngine;

public class Sun : MonoBehaviour
{
    public Transform sun;

    public float timeInSeconds = 180f;
    private float elapsedTime = 0f;

    public TMPro.TMP_Text timeText;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float sunAngle = Mathf.Lerp(-170f, 5f, elapsedTime / timeInSeconds);
        sun.transform.rotation = Quaternion.Euler(sunAngle, 0f, 0f);

        timeText.text = Mathf.FloorToInt(elapsedTime).ToString();

        //시간 다 지남
        if (elapsedTime >= timeInSeconds)
        {
            elapsedTime = 0f;
        }
    }
}
