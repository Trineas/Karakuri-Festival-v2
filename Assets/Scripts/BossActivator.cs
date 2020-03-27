using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject entrance, boss;

    public static BossActivator instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            entrance.SetActive(false);
            boss.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
