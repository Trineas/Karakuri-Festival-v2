using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator anim;

    public ButtonController button;
    
    void Update()
    {
        if (button.isPressed)
        {
            anim.SetBool("Open", true);
            anim.SetBool("Close", false);
        }

        else
        {
            anim.SetBool("Close", true);
            anim.SetBool("Open", false);
        }
    }
}
