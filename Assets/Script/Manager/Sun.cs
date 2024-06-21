using UnityEngine;

public class Sun : MonoBehaviour
{
    public Transform sun;

    public float timeInSeconds = 180f;
    private float elapsedTime = 0f;

    public TMPro.TMP_Text timeText;

    void Update()
    {
        elapsedTime -= Time.deltaTime; // -= 연산자로 감소

        float sunAngle = Mathf.Lerp(5f, -170f, elapsedTime / timeInSeconds);
        sun.transform.rotation = Quaternion.Euler(sunAngle, 0f, 0f);

        timeText.text = "남은 시간:" + Mathf.FloorToInt(elapsedTime).ToString();

        // 시간이 0 이하로 내려갔을 때 초기화
        if (elapsedTime <= 0f)
        {
            elapsedTime = timeInSeconds;
        }
    }
}
