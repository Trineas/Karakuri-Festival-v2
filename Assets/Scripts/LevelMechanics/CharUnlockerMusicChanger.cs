using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharUnlockerMusicChanger : MonoBehaviour
{
    public int charTheme;
    private bool isThemePlaying;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isThemePlaying)
        {
            AudioManager.instance.PlayMusic(charTheme);
            isThemePlaying = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.instance.PlayMusic(AudioManager.instance.levelMusicToPlay);
        }
    }
}
