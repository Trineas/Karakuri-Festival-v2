using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door, openRot;

    public float openSpeed;

    private Quaternion startRot;

    public ButtonController button;
    
    void Start()
    {
        startRot = transform.rotation;
    }

    void Update()
    {
        if (button.isPressed)
        {
            door.rotation = Quaternion.Slerp(door.rotation, openRot.rotation, openSpeed * Time.deltaTime);
        }

        else
        {
            door.rotation = Quaternion.Slerp(door.rotation, startRot, openSpeed * Time.deltaTime);
        }
    }
}
