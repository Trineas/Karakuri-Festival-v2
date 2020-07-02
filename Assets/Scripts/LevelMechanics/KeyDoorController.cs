using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorController : MonoBehaviour
{
    public Animator lockAnim, doorAnim;

    public BoxCollider col;

    private bool isUnlocked;

    public int errorSound, soundToPlay;

    void Update()
    {
        if (isUnlocked)
        {
            doorAnim.SetBool("Open", true);
            doorAnim.SetBool("Close", false);
        }

        else
        {
            doorAnim.SetBool("Close", true);
            doorAnim.SetBool("Open", false);
        }
    }

    IEnumerator UnlockCo()
    {
        lockAnim.SetBool("Unlock", true);
        col.size = new Vector3(0.01f, 0.01f, 0.01f);
        GameManager.instance.SubtractKeys();
        yield return new WaitForSeconds(1f);
        AudioManager.instance.PlaySFX(soundToPlay);
        isUnlocked = true;
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    void Unlock()
    {
        StartCoroutine(UnlockCo());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && GameManager.instance.currentKeys >= 1)
        {
            Unlock();
        }

        else
        {
            AudioManager.instance.PlaySFX(errorSound);
        }
    }
}
