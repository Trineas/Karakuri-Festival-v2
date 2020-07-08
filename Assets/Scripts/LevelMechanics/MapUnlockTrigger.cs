using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUnlockTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.mapUnlocked = true;
            Destroy(this.gameObject);
        }
    }
}
