using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 0.0f;
    }
    public void StartButtonClicked()
    {
        foreach (Transform child in transform)
        {
            if (!(child.name == "Score" || child.name == "StaticScore"))
            {
                Debug.Log("Child found. Name: " + child.name);

                child.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}
