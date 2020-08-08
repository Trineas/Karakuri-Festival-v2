using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerSamurai : MonoBehaviour
{
    public static DialogueTriggerSamurai instance;

    public SpeechScript samuraiSpeech = null;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartSpeech(samuraiSpeech);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = true;
            PlayerController.instance.canTalkToSamurai = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = false;
            PlayerController.instance.canTalkToSamurai = false;
        }
    }
}
