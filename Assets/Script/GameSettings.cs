using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;
    
    private GameObject gameOverPopUp;
    private int levelCount = 1;
    private AudioSource audioSource;

    private bool soundOn = true;
    
    private void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else Destroy(this);
    }

    void Start()
    {
        levelCount = 1;
        audioSource = GetComponent<AudioSource>();
    }


    public void NewLevel()
    {
        levelCount++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioSource.Play();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        audioSource.Play();
    }

    public int GetLevelCount()
    {
        return levelCount;
    }

    public void RestartGame()
    {
        levelCount = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GetGameOverPopUp(GameObject gameObj) 
    {
        gameOverPopUp = gameObj;
    }

    public void OnGameOver()
    {
        gameOverPopUp.gameObject.SetActive(true);
    }

    public bool GetSoundState()
    {
        return soundOn;
    }

    public void SetSoundState(bool soundState)
    {
        soundOn = soundState;
    }

}
