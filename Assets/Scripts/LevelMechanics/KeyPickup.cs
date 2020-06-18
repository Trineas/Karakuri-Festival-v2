using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject pickupEffect;

    public int soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.AddKeys();

            Destroy(gameObject);
            Instantiate(pickupEffect, transform.position, transform.rotation);

            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
