using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIManager : MonoBehaviour
{
    public static LSUIManager instance;

    public Text levelNameText;
    public GameObject levelNamePanel;

    public Text coinsText;

    private void Awake()
    {
        instance = this;
    }
}
