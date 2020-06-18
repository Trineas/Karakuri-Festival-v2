using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharUnlocker : MonoBehaviour
{
    public static CharUnlocker instance;
    public GameObject musicTrigger;
    public GameObject unlockText;

    public int soundToPlay;

    private void Awake()
    {
        instance = this;
    }

    IEnumerator UnlockCo()
    {
        AudioManager.instance.PlaySFX(soundToPlay);
        CharacterSwitch.instance.isCharacter3Unlocked = true;
        musicTrigger.SetActive(false);
        unlockText.SetActive(true);

        yield return new WaitForSeconds(4f);

        AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);
        PlayerController.instance.isInteracting = false;
        PlayerController.instance.stopMove = false;
        unlockText.SetActive(false);

        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    public void Unlock()
    {
        StartCoroutine(UnlockCo());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canInteractWith = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canInteractWith = false;
        }
    }
}
