using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public static LevelExit instance;

    public Animator anim;

    public string levelToLoad;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator LevelEndCo()
    {
        //AudioManager.instance.PlayMusic(LevelEndMusic);

        PlayerController.instance.stopMove = true;

        yield return new WaitForSeconds(1.25f);

        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(1.25f);

        SceneManager.LoadScene(levelToLoad);
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("Hit");

            StartCoroutine(LevelEndCo());
        }
    }
}
