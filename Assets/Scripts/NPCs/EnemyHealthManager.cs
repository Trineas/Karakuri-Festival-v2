using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public float invincibleLength = 1f;
    private float invincCounter;

    public int deathSound;

    public GameObject deathEffect, itemToDrop;

    public GameObject[] enemyPieces;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            for (int i = 0; i < enemyPieces.Length; i++)
            {
                if (Mathf.Floor(invincCounter * 5f) % 2 == 0)
                {
                    enemyPieces[i].SetActive(true);
                }
                else
                {
                    enemyPieces[i].SetActive(false);
                }

                if (invincCounter <= 0)
                {
                    enemyPieces[i].SetActive(true);
                }
            }
        }
    }

    public void TakeDamage()
    {
        if (invincCounter <= 0)
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

            else if (CharacterSwitch.instance.currentCharacter == 3)
            {
                currentHealth--;
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                AudioManager.instance.PlaySFX(deathSound);

                Destroy(gameObject);

                //PlayerController.instance.Bounce();

                Instantiate(deathEffect, transform.position + new Vector3(0f, 1.2f, 0f), transform.rotation);
                Instantiate(itemToDrop, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation);
            }

            else
            {
                invincCounter = invincibleLength;
            }
        }
    }
}
