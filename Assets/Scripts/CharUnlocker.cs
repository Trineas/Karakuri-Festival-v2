using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharUnlocker : MonoBehaviour
{
    public static CharUnlocker instance;

    private void Awake()
    {
        instance = this;
    }

    public void Unlock()
    {
        CharacterSwitch.instance.isCharacter3Unlocked = true;
        Destroy(this.gameObject);

        PlayerController.instance.isInteracting = false;
        PlayerController.instance.stopMove = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canInteractWith = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canInteractWith = false;
        }
    }
}
