using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
#region Singleton
    public static GamePlayManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else Destroy(this);
    }
#endregion
    private GameEvents gameEvents;
    public GameObject gameOverPopUp;
    public GameObject levelCompletedPopUp;
    private int levelCount;
    public GameObject shipGameObject;
 
    void Start()
    {
        levelCount = 1;
        
        gameEvents = GameEvents.Instance;
        GameEvents.OnLastEnemyKilled += NewLevel;
        GameEvents.OnGameOver += GameOver;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {RestartLevel();}
    }

    private void OnDestroy()
    {
        GameEvents.OnLastEnemyKilled -= NewLevel;
        GameEvents.OnGameOver -= GameOver;
    }

    public void NewLevel()
    {
        //Increasing level count and triggering Next Level game event
        levelCount++;
        gameEvents.TriggerNextLevel(levelCount);
        
        DisplayLevelCompletedText();
        
        Debug.Log("New Level: "+levelCount);
    }

    public void RestartLevel()
    {
        //Triggering Next Level game event with current level count to Restart Level
        gameEvents.TriggerNextLevel(levelCount);

        Debug.Log("Level Restarted: "+levelCount);
    }

    public void GameOver()
    {
        gameOverPopUp.gameObject.SetActive(true);
        
        Debug.Log("Game Over");
    }

    public void RestartGame()
    {
        levelCount = 1;
        
        gameEvents.TriggerGameRestart();

        shipGameObject.SetActive(true);
        Debug.Log("Game Restarted");
    }

    private void DisplayLevelCompletedText()
    {
        levelCompletedPopUp.gameObject.SetActive(true);
    }

}
