using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerMerchant : MonoBehaviour
{
    public static DialogueTriggerMerchant instance;

    public SpeechScript merchantSpeech = null;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartSpeech(merchantSpeech);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = true;
            PlayerController.instance.canTalkToMerchant = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = false;
            PlayerController.instance.canTalkToMerchant = false;
        }
    }
}
