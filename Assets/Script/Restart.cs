using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour, IPointerClickHandler
{

    void Update()
    {
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
