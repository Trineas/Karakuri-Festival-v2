using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFloorElevatorCaller : MonoBehaviour
{
    public Animator anim;
    private bool isCalling;

    private void OnTriggerEnter(Collider other)
    {
        isCalling = true;

        if (other.tag == "Player" && !anim.GetCurrentAnimatorStateInfo(0).IsName("Elevator_1") && isCalling)
        {
            anim.SetTrigger("2-1");
            isCalling = false;
        }
    }
}
