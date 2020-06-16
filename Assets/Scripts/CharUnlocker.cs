using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharUnlocker : MonoBehaviour
{
    public static CharUnlocker instance;
    public GameObject elevatorDoor;

    private void Awake()
    {
        instance = this;
    }

    public void Unlock()
    {
        CharacterSwitch.instance.isCharacter3Unlocked = true;
        this.gameObject.SetActive(false);
        elevatorDoor.SetActive(true);

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
