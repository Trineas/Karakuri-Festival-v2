using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerWoodcutter : MonoBehaviour
{
    public static DialogueTriggerWoodcutter instance;

    public SpeechScript woodcutterSpeech = null;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartSpeech(woodcutterSpeech);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = true;
            PlayerController.instance.canTalkToWoodcutter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.canTalkTo = false;
            PlayerController.instance.canTalkToWoodcutter = false;
        }
    }
}
