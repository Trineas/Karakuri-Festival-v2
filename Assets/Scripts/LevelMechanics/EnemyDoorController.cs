using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoorController : MonoBehaviour
{
    public static EnemyDoorController instance;

    public Animator gateAnim, doorAnim;

    public int soundToPlay, enemiesDefeatedSound;

    public bool isUnlocked;

    public int enemyCounter;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (enemyCounter <= 0)
        {
            //AudioManager.instance.PlaySFX(enemiesDefeatedSound);
            Unlock();
        }

        if (enemyCounter < 0)
        {
            enemyCounter = 0;
        }

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
        gateAnim.SetBool("Unlock", true);
        yield return new WaitForSeconds(1f);
        //AudioManager.instance.PlaySFX(soundToPlay);
        isUnlocked = true;
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    void Unlock()
    {
        StartCoroutine(UnlockCo());
    }
}
