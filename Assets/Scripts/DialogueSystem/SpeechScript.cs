using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Speech", menuName = "Dialogue/Speech")]
public class SpeechScript : ScriptableObject
{
    //public int speechNum;

    //public string speechName;

    public List<DialogueManager.SpeechGroup> speechGroup;
}
