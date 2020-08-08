using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerBlacksmith : MonoBehaviour
{
    public static DialogueTriggerBlacksmith instance;

    public SpeechScript blacksmithSpeech = null;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartSpeech(blacksmithSpeech);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = true;
            PlayerController.instance.canTalkToBlacksmith = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = false;
            PlayerController.instance.canTalkToBlacksmith = false;
        }
    }
}
