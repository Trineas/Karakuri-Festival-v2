using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public enum Speaker { Taku, Baku, Satsu, Samurai }

    [System.Serializable]
    public struct SpeechGroup
    {
        public Speaker currentSpeaker;
        //public int speechSound;
        [TextArea(2, 5)]
        public string speechText;
    }

    public static DialogueManager instance;

    public int typingSound;

    public GameObject continueIcon;

    SpeechScript currentSpeechFull;
    int currentSpeechIndex;
    public bool speechInProgress = false;
    string currentSpeechText = "";
    float fillAmount = 0f;
    bool running = false;

    public Sprite[] artworks;

    [SerializeField] CanvasGroup TextBoxCanvasGroup = null;
    [SerializeField] Image speakerPortrait = null;
    [SerializeField] Text speakerName = null;
    [SerializeField] Text mainText = null;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (running)
        {
            fillAmount += Time.deltaTime * 50;

            if (fillAmount < currentSpeechText.Length)
            {
                mainText.text = currentSpeechText.Substring(0, (int)fillAmount);
                continueIcon.SetActive(false);
            }

            else
            {
                running = false;
                mainText.text = currentSpeechText;
                continueIcon.SetActive(true);
            }
        }
    }

    public void StartSpeech(SpeechScript speech, float delay = 0f)
    {
        StartCoroutine(StartSpeechCo(speech, delay));
    }

    public void EndSpeech()
    {
        StartCoroutine(EndSpeechCo());
    }

    IEnumerator StartSpeechCo(SpeechScript speech, float delay = 0f)
    {
        while (speechInProgress)
        {
            yield return null;
        }

        speechInProgress = true;

        yield return new WaitForSeconds(delay);

        currentSpeechFull = speech;
        currentSpeechIndex = 0;
        FillSpeech();
        AudioManager.instance.PlaySFX(typingSound);
        float cgrpAlpha = 0f;

        while (cgrpAlpha < 1f)
        {
            cgrpAlpha += Time.deltaTime  * 2;
            TextBoxCanvasGroup.alpha = cgrpAlpha;
            yield return null;
        }

        TextBoxCanvasGroup.alpha = 1;
        TextBoxCanvasGroup.blocksRaycasts = TextBoxCanvasGroup.interactable = true;
    }

    IEnumerator EndSpeechCo(float delay = 0.25f)
    {
        speechInProgress = false;

        TextBoxCanvasGroup.blocksRaycasts = TextBoxCanvasGroup.interactable = false;
        float cgrpAlpha = 1f;
        while (cgrpAlpha > 0)
        {
            cgrpAlpha -= Time.deltaTime * 2;
            TextBoxCanvasGroup.alpha = cgrpAlpha;
            yield return null;
        }

        yield return new WaitForSeconds(delay);

        PlayerController.instance.isInteracting = false;
        PlayerController.instance.stopMove = false;
    }

    public void SetSpeechNext()
    {
        currentSpeechIndex++;

        if (currentSpeechFull.speechGroup.Count > currentSpeechIndex)
        {
            FillSpeech();
            AudioManager.instance.PlaySFX(typingSound);
        }

        else
        {
            EndSpeech();
        }
    }

    public void FillSpeech()
    {
        SpeechGroup s = currentSpeechFull.speechGroup[currentSpeechIndex];
        speakerPortrait.sprite = artworks[(int)s.currentSpeaker];
        speakerName.text = s.currentSpeaker.ToString();
        currentSpeechText = s.speechText;
        running = true;
        fillAmount = 0;

        //if (s.speechSound != null)
        //{
        //    s.speechSound = ;
        //}
    }
}
