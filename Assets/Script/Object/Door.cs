using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public GameObject door;
    private Vector3 openDoor = new Vector3(0, 90, 0);
    private Vector3 closeDoor = new Vector3(0, 0, 0);

    public void ChangeIsOpen()
    {
        isOpen = !isOpen;
    }

    void Update()
    {
        if(isOpen) door.transform.DORotate(openDoor, 0.5f).SetEase(Ease.Linear);
        else door.transform.DORotate(closeDoor, 0.5f).SetEase(Ease.Linear);
    }
}
