﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public int deathSound;

    public GameObject deathEffect, itemToDrop;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        if (CharacterSwitch.instance.currentCharacter == 1)
        {
            currentHealth--;
            currentHealth--;
        }

        else if (CharacterSwitch.instance.currentCharacter == 2)
        {
            currentHealth--;
            currentHealth--;
            currentHealth--;
        }

        else
        {
            currentHealth--;
        }


        if (currentHealth <= 0)
        {
            AudioManager.instance.PlaySFX(deathSound);

            Destroy(gameObject);

            PlayerController.instance.Bounce();

            Instantiate(deathEffect, transform.position + new Vector3(0f, 1.2f, 0f), transform.rotation);
            Instantiate(itemToDrop, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation);
        }
    }
}
