using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCount : MonoBehaviour
{
    public GameObject Text;

    private int levelCount;

    private void Start()
    {
        levelCount = GameSettings.Instance.GetLevelCount();

        Text.GetComponent<Text>().text = "Level: " + levelCount;
    }

}
