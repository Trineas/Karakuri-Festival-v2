using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public int soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.Respawn();
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
