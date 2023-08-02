using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    private Scene currentScene;

    [Header("Game Settings")]
    private bool isPaused = false;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentScene.name);
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            PauseScene(ref isPaused);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene(currentScene.name);
            Time.timeScale = 0;
        }
        if(Time.timeScale == 0)
        {
            isPaused = true;
        }
    }

    private void PauseScene(ref bool pauseState)
    {
        if(!pauseState)
        {
            Time.timeScale = 0;
            pauseState = true;
            return;
        }
        if(pauseState)
        {
            Time.timeScale = 1;
            pauseState = false;
        }
    }
}
