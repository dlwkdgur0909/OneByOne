using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource walking;
    public AudioSource openDoor;
    public AudioSource closeDoor;
    public AudioSource CCTV;
    
    public AudioSource shoot;
    public AudioSource cctvShoot;
    public AudioSource reload;

    public AudioSource cannonShoot;
    public AudioSource cannonReload;
    public AudioSource boom;

    public AudioSource buy;
    public AudioSource InsufficientGold;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
