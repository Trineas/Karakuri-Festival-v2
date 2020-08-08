using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerDamasuko : MonoBehaviour
{
    public static DialogueTriggerDamasuko instance;

    public SpeechScript damasukoSpeech = null;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartSpeech(damasukoSpeech);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = true;
            PlayerController.instance.canTalkToDamasuko = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = false;
            PlayerController.instance.canTalkToDamasuko = false;
        }
    }
}
