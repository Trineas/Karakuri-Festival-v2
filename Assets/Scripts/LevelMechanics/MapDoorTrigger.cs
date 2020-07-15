using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDoorTrigger : MonoBehaviour
{
    public GameObject roomToUnlock;

    private void OnTriggerEnter(Collider other)
    {
        BoxCollider col = GetComponent<BoxCollider>();

        roomToUnlock.SetActive(true);
        col.enabled = false;
    }
}
