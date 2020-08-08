using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerVillageGuard : MonoBehaviour
{
    public static DialogueTriggerVillageGuard instance;

    public SpeechScript villageGuardSpeech = null;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartSpeech(villageGuardSpeech);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = true;
            PlayerController.instance.canTalkToVillageGuard = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = false;
            PlayerController.instance.canTalkToVillageGuard = false;
        }
    }
}
