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
        levelCount++;
        gameEvents.TriggerNextLevel(levelCount);

        DisplayLevelCompletedText();
        
    }

    public void RestartLevel()
    {
        gameEvents.TriggerNextLevel(levelCount);
    }

    public void GameOver()
    {
        gameOverPopUp.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        levelCount = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void DisplayLevelCompletedText()
    {
        levelCompletedPopUp.gameObject.SetActive(true);
    }

}
