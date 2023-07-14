using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        GamePlayManager.Instance.RestartGame();
        this.gameObject.SetActive(false);
    }


}
