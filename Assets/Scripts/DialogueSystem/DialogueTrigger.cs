using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public static DialogueTrigger instance;

    public bool canTalkTo;

    [SerializeField] SpeechScript speechToPlay;

    public int eventSoundToPlay;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        canTalkTo = false;
    }

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartSpeech(speechToPlay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canTalkTo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canTalkTo = false;
        }
    }
}
