using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HealthManager.instance.Hurt();
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
