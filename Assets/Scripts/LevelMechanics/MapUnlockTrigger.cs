using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnlockTrigger : MonoBehaviour
{
    public int soundToplay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.instance.PlaySFX(soundToplay);
            GameManager.instance.mapUnlocked = true;
            Destroy(this.gameObject);
        }
    }
}
