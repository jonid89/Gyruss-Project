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
        
        //Subscribing to OnNextLevel event to Set Level
        gameEvents = GameEvents.Instance;
        GameEvents.OnNextLevel += SetLevel;
    }

    private void OnDestroy()
    {
        GameEvents.OnNextLevel -= SetLevel;
    }

    public void SetLevel(int newLevel){
        currentLevel = newLevel;
        levelText.text = "Level: " + currentLevel.ToString();
    }

}
