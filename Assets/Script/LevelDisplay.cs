using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    private int currentLevel;
    private GameEvents gameEvents;

    private void Start()
    {
        currentLevel = 1;
        SetLevel(currentLevel);
        
        //Subscribing to Game Events
        gameEvents = GameEvents.Instance;
        GameEvents.OnNextLevel += SetLevel;
        GameEvents.OnGameRestart += ResetLevel;
    }

    private void OnDestroy()
    {
        GameEvents.OnNextLevel -= SetLevel;
        GameEvents.OnGameRestart -= ResetLevel;
    }

    public void SetLevel(int newLevel){
        currentLevel = newLevel;
        DisplayLevel();
    }

    private void ResetLevel()
    {
        currentLevel = 1;
        DisplayLevel();
    }

    private void DisplayLevel()
    {
        levelText.text = "Level: " + currentLevel.ToString();
    }

}
