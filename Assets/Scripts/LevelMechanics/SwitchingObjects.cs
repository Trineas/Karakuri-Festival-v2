using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingObjects : MonoBehaviour
{
    public GameObject objectHolder;

    public ButtonController button;

    public bool revealWhenPressed;

    void Start()
    {
        
    }

    void Update()
    {
        if (button.isPressed)
        {
            objectHolder.SetActive(revealWhenPressed);
        }

        else
        {
            objectHolder.SetActive(!revealWhenPressed);
        }
    }
}
