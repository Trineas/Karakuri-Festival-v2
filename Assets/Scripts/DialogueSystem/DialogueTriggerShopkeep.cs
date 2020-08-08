using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerShopkeep : MonoBehaviour
{
    public static DialogueTriggerShopkeep instance;

    public SpeechScript shopKeepSpeech = null;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartSpeech(shopKeepSpeech);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = true;
            PlayerController.instance.canTalkToShopkeep = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = false;
            PlayerController.instance.canTalkToShopkeep = false;
        }
    }
}
