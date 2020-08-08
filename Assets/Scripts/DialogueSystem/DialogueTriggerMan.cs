using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerMan : MonoBehaviour
{
    public static DialogueTriggerMan instance;

    public SpeechScript manSpeech = null;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartSpeech(manSpeech);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = true;
            PlayerController.instance.canTalkToMan = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = false;
            PlayerController.instance.canTalkToMan = false;
        }
    }
}
