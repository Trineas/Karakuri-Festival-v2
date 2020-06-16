using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorUnlocker : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("3-2");
            this.gameObject.SetActive(false);
        }
    }
}
