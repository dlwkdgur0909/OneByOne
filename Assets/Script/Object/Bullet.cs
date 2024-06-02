using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int DMG = 0;
    public float speed;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
