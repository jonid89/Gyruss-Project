using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotsController : MonoBehaviour
{
    private GameEvents gameEvents;

    void Start()
    {
        //Subscribing to Game Events
        gameEvents = GameEvents.Instance;
        GameEvents.OnGameOver += DeactivateShot;
        GameEvents.OnNextLevel += OnNextLevel;
    }

    private void OnDestroy()
    {
        GameEvents.OnGameOver -= DeactivateShot;
        GameEvents.OnNextLevel -= OnNextLevel;
    }

    private void DeactivateShot()
    {
        this.gameObject.SetActive(false);
    }

    private void OnNextLevel(int level)
    {
        DeactivateShot();
    }

}
