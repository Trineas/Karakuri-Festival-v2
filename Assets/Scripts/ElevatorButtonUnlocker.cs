using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButtonUnlocker : MonoBehaviour
{
    public GameObject oldButton;
    public GameObject newButton;
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("3-4");
            newButton.SetActive(true);
            this.gameObject.SetActive(false);
            Destroy(oldButton.gameObject);
        }
    }
}
