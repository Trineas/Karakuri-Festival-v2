using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public float invincibleLength = 1f;
    private float invincCounter;

    public bool isTakingDamage;

    public bool miniboss;
    public GameObject charUnlocker;
    public int unlockMusic;

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
                    isTakingDamage = false;
                }
            }
        }
    }

    public void TakeDamage()
    {
        if (invincCounter <= 0)
        {
            isTakingDamage = true;

            if (CharacterSwitch.instance.currentCharacter == 1)
            {
                currentHealth -= 2;
            }

            else if (CharacterSwitch.instance.currentCharacter == 2)
            {
                currentHealth -= 3;
            }

            else if (CharacterSwitch.instance.currentCharacter == 3)
            {
                currentHealth -= 1;
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                AudioManager.instance.PlaySFX(deathSound);

                Destroy(gameObject);

                if (miniboss)
                {
                    AudioManager.instance.PlayMusic(unlockMusic);
                    charUnlocker.transform.position = this.transform.position;
                    charUnlocker.transform.rotation = this.transform.rotation;
                    charUnlocker.SetActive(true);

                }
                else
                {
                    Instantiate(deathEffect, transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
                    Instantiate(itemToDrop, transform.position + new Vector3(0f, 0.5f, 0f), transform.rotation);
                }
            }

            else
            {
                invincCounter = invincibleLength;
            }
        }
    }
}
