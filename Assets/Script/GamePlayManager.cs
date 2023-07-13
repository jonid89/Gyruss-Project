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
    private int levelCount;
 

    void Start()
    {
        levelCount = 1;
        
        gameEvents = GameEvents.Instance;
        GameEvents.OnLastEnemyKilled += NewLevel;
    }

    private void OnDestroy()
    {
        GameEvents.OnLastEnemyKilled -= NewLevel;
    }

    public void NewLevel()
    {
        levelCount++;
        gameEvents.TriggerNextLevel(levelCount);
    }

    public void RestartLevel()
    {
        gameEvents.TriggerNextLevel(levelCount);
    }

    public void RestartGame()
    {
        levelCount = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
