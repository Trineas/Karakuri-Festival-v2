using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public bool isPressed;

    public Transform button, buttonDown;

    private Vector3 buttonUp;

    public bool isOnOff;

    public int soundToPlay;

    void Start()
    {
        buttonUp = button.position;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (isOnOff)
            {
                if (isPressed)
                {
                    AudioManager.instance.PlaySFX(soundToPlay);
                    button.position = buttonUp;
                    isPressed = false;
                }

                else
                {
                    AudioManager.instance.PlaySFX(soundToPlay);
                    button.position = buttonDown.position;
                    isPressed = true;
                }
            }

            else
            {
                if (!isPressed)
                {
                    AudioManager.instance.PlaySFX(soundToPlay);
                    button.position = buttonDown.position;
                    isPressed = true;
                }
            }
        }
    }
}
