using UnityEngine;

public class Sun : MonoBehaviour
{
    public Transform sun;

    public float timeInSeconds = 180f;
    private float elapsedTime = 0f;

    public TMPro.TMP_Text timeText;

    void Update()
    {
        elapsedTime -= Time.deltaTime; // -= �����ڷ� ����

        float sunAngle = Mathf.Lerp(5f, -170f, elapsedTime / timeInSeconds);
        sun.transform.rotation = Quaternion.Euler(sunAngle, 0f, 0f);

        timeText.text = "���� �ð�:" + Mathf.FloorToInt(elapsedTime).ToString();

        // �ð��� 0 ���Ϸ� �������� �� �ʱ�ȭ
        if (elapsedTime <= 0f)
        {
            elapsedTime = timeInSeconds;
        }
    }
}
