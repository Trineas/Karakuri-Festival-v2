using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    public static CharacterSwitch instance;

    public int currentCharacter;
    public bool isCharacter2Unlocked, isCharacter3Unlocked;

    public bool char1Enabled, char2Enabled, char3Enabled;

    public GameObject charSwitchEffect;

    public int soundToPlay1, soundToPlay2, soundToPlay3;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        char1Enabled = true;
        char2Enabled = false;
        char3Enabled = false;
    }

    void Update()
    {
       if (char1Enabled)
        {
            currentCharacter = 1;
        }

        if (char2Enabled)
        {
            currentCharacter = 2;
        }

        if (char3Enabled)
        {
            currentCharacter = 3;
        }
    }

    public void CharSwitchRight()
    {
        StartCoroutine(CharSwitchRightCo());
    }

    public void CharSwitchLeft()
    {
        StartCoroutine(CharSwitchLeftCo());
    }

    public IEnumerator CharSwitchRightCo()
    {
        if (char1Enabled && !char2Enabled && !char3Enabled)
        {
            if (isCharacter2Unlocked)
            {
                AudioManager.instance.PlaySFX(soundToPlay2);

                PlayerController.instance.stopMove = true;
                PlayerController.instance.isInteracting = true;

                Instantiate(charSwitchEffect, transform.position, transform.rotation);

                yield return new WaitForSeconds(0.5f);

                char1Enabled = false;
                char2Enabled = true;
                char3Enabled = false;

                PlayerController.instance.stopMove = false;
                PlayerController.instance.isInteracting = false;
            }
        }

        else if (!char1Enabled && char2Enabled && !char3Enabled)
        {
            if (isCharacter3Unlocked)
            {
                AudioManager.instance.PlaySFX(soundToPlay3);

                PlayerController.instance.stopMove = true;
                PlayerController.instance.isInteracting = true;

                Instantiate(charSwitchEffect, transform.position, transform.rotation);

                yield return new WaitForSeconds(0.5f);

                char1Enabled = false;
                char2Enabled = false;
                char3Enabled = true;

                PlayerController.instance.stopMove = false;
                PlayerController.instance.isInteracting = false;
            }

            else
            {
                AudioManager.instance.PlaySFX(soundToPlay1);

                PlayerController.instance.stopMove = true;
                PlayerController.instance.isInteracting = true;

                Instantiate(charSwitchEffect, transform.position, transform.rotation);

                yield return new WaitForSeconds(0.5f);

                char1Enabled = true;
                char2Enabled = false;
                char3Enabled = false;

                PlayerController.instance.stopMove = false;
                PlayerController.instance.isInteracting = false;

            }
        }

        else if (char3Enabled)
        {
            if (isCharacter3Unlocked)
            {
                AudioManager.instance.PlaySFX(soundToPlay1);

                PlayerController.instance.stopMove = true;
                PlayerController.instance.isInteracting = true;

                Instantiate(charSwitchEffect, transform.position, transform.rotation);

                yield return new WaitForSeconds(0.5f);

                char1Enabled = true;
                char2Enabled = false;
                char3Enabled = false;

                PlayerController.instance.stopMove = false;
                PlayerController.instance.isInteracting = false;
            }
        }
    }

    public IEnumerator CharSwitchLeftCo()
    {
        if (char1Enabled && !char2Enabled && !char3Enabled)
        {
            if (isCharacter3Unlocked)
            {
                AudioManager.instance.PlaySFX(soundToPlay3);

                PlayerController.instance.stopMove = true;
                PlayerController.instance.isInteracting = true;

                Instantiate(charSwitchEffect, transform.position, transform.rotation);

                yield return new WaitForSeconds(0.5f);

                char1Enabled = false;
                char2Enabled = false;
                char3Enabled = true;

                PlayerController.instance.stopMove = false;
                PlayerController.instance.isInteracting = false;
            }

            else if (isCharacter2Unlocked)
            {
                AudioManager.instance.PlaySFX(soundToPlay2);

                PlayerController.instance.stopMove = true;
                PlayerController.instance.isInteracting = true;

                Instantiate(charSwitchEffect, transform.position, transform.rotation);

                yield return new WaitForSeconds(0.5f);

                char1Enabled = false;
                char2Enabled = true;
                char3Enabled = false;

                PlayerController.instance.stopMove = false;
                PlayerController.instance.isInteracting = false;
            }
        }

        else if (char2Enabled)
        {
            AudioManager.instance.PlaySFX(soundToPlay1);

            PlayerController.instance.stopMove = true;
            PlayerController.instance.isInteracting = true;

            Instantiate(charSwitchEffect, transform.position, transform.rotation);

            yield return new WaitForSeconds(0.5f);

            char1Enabled = true;
            char2Enabled = false;
            char3Enabled = false;

            PlayerController.instance.stopMove = false;
            PlayerController.instance.isInteracting = false;
        }

        else if (char3Enabled)
        {
            if (isCharacter3Unlocked)
            {
                AudioManager.instance.PlaySFX(soundToPlay2);

                PlayerController.instance.stopMove = true;
                PlayerController.instance.isInteracting = true;

                Instantiate(charSwitchEffect, transform.position, transform.rotation);

                yield return new WaitForSeconds(0.5f);

                char1Enabled = false;
                char2Enabled = true;
                char3Enabled = false;

                PlayerController.instance.stopMove = false;
                PlayerController.instance.isInteracting = false;
            }
        }
    }
}
