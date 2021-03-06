﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorControllerDown : MonoBehaviour
{
    public static ElevatorControllerDown instance;

    public Transform button, buttonDown;

    private Vector3 buttonUp;

    public int soundToPlay;

    public int currentFloor;

    public Animator anim;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        buttonUp = button.position;
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Elevator_1"))
        {
            currentFloor = 1;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Elevator_2"))
        {
            currentFloor = 2;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Elevator_3"))
        {
            currentFloor = 3;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Elevator_4"))
        {
            currentFloor = 4;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Elevator_5"))
        {
            currentFloor = 5;
        }
    }

    IEnumerator ButtonPressCo()
    {
        AudioManager.instance.PlaySFX(soundToPlay);
        button.position = buttonDown.position;
        yield return new WaitForSeconds(0.1f);
        button.position = buttonUp;

        yield return new WaitForSeconds(1f);
        PlayerController.instance.isInteracting = false;
        PlayerController.instance.stopMove = false;
    }

    public void MoveElevator()
    {
        if (currentFloor == 2)
        {
            anim.SetTrigger("2-1");
        }

        if (currentFloor == 3)
        {
            anim.SetTrigger("3-2");
        }

        if (currentFloor == 4)
        {
            anim.SetTrigger("4-3");
        }

        if (currentFloor == 5)
        {
            anim.SetTrigger("5-4");
        }

        StartCoroutine(ButtonPressCo());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canInteractWithDown = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canInteractWithDown = false;
        }
    }
}
