using UnityEngine;

public class MainDoor : MonoBehaviour
{
    public static int doorHP = 100;

    public TMPro.TMP_Text HPText;

    private void Update()
    {
        HPText.text = "내구도:" + doorHP.ToString();

        if (doorHP <= 0)
        {
            //문이 부숴졌습니다 띄우기
            GameManager.instance.frontDoor = null;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int Damege)
    {
        doorHP -= Damege;
    }
}
