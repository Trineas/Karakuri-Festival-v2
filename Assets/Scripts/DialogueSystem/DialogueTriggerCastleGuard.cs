using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerCastleGuard : MonoBehaviour
{
    public static DialogueTriggerCastleGuard instance;

    public SpeechScript castleGuardSpeech = null;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartSpeech(castleGuardSpeech);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = true;
            PlayerController.instance.canTalkToCastleGuard = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = false;
            PlayerController.instance.canTalkToCastleGuard = false;
        }
    }
}
