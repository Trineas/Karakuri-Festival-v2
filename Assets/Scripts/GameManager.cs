using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 respawnPosition;

    public GameObject deathEffect;

    public int currentCoins, currentKeys;

    public int LevelEndMusic = 8;

    public bool isRespawning, isInDungeon, mapUnlocked;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        respawnPosition = PlayerController.instance.transform.position;

        AddCoins(0);
        currentKeys = 0;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "04_Castle")
        {
            isInDungeon = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            PauseUnpause();
        }

        if (mapUnlocked)
        {
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Joystick1Button6))
            {
                OpenCloseMap();
            }
        }

        if (isInDungeon)
        {
            UIManager.instance.keyImage.gameObject.SetActive(true);
            UIManager.instance.keyText.gameObject.SetActive(true);
        }

        else
        {
            UIManager.instance.keyImage.gameObject.SetActive(false);
            UIManager.instance.keyText.gameObject.SetActive(false);
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());

        HealthManager.instance.PlayerKilled();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCo());

        HealthManager.instance.PlayerKilled();
    }

    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);
        CameraController.instance.cmBrain.enabled = false;

        UIManager.instance.fadeToBlack = true;
        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);

        yield return new WaitForSeconds(2f);

        isRespawning = true;

        HealthManager.instance.ResetHealth();
        UIManager.instance.fadeFromBlack = true;
        PlayerController.instance.transform.position = respawnPosition;
        CameraController.instance.cmBrain.enabled = true;
        PlayerController.instance.gameObject.SetActive(true);
    }

    public IEnumerator GameOverCo()
    {
        UIManager.instance.fadeToBlack = true;
        Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);

        yield return new WaitForSeconds(2f);

        HealthManager.instance.currentLives = 3;
        SceneManager.LoadScene("04_Castle");
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.coinText.text = "" + currentCoins;
    }

    public void SubtractCoins()
    {
        currentCoins--;
        UIManager.instance.coinText.text = "" + currentCoins;
    }

    public void AddKeys()
    {
        currentKeys++;
        UIManager.instance.keyText.text = "x " + currentKeys;
    }

    public void SubtractKeys()
    {
        currentKeys--;
        UIManager.instance.keyText.text = "x " + currentKeys;
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;

            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }

        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(UIManager.instance.pauseFirstButton);
            

            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OpenCloseMap()
    {
        if (UIManager.instance.mapScreen.activeInHierarchy)
        {
            UIManager.instance.mapScreen.SetActive(false);
            Time.timeScale = 1f;

            //Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }

        else
        {
            UIManager.instance.mapScreen.SetActive(true);
            Time.timeScale = 0f;

            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }
    }

    /* public IEnumerator LevelEndCo()
    {
        //AudioManager.instance.PlayMusic(LevelEndMusic);

        PlayerController.instance.stopMove = true;

        yield return new WaitForSeconds(1.25f);

        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(1.25f);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_coins"))
        {
            if (currentCoins > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_coins"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", currentCoins);
            }
        }

        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", currentCoins);
        }

        SceneManager.LoadScene(LevelExit.instance.levelToLoad);
    }
    */
}
