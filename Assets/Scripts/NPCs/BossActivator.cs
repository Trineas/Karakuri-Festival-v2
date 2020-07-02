using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject entrance, exitZone, boss;

    public static BossActivator instance;

    public int soundToPlay;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            entrance.SetActive(true);
            exitZone.SetActive(false);
            AudioManager.instance.PlaySFX(soundToPlay);
            boss.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
