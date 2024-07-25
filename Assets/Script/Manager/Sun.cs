using UnityEngine;

public class Sun : MonoBehaviour
{
    public Transform sun;

    public float timeInSeconds = 180f;
<<<<<<< HEAD
    private float elapsedTime = 0f;
=======
    public float elapsedTime = 0.1f;
>>>>>>> 192c12c (Game Clear and Game over 2)

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
