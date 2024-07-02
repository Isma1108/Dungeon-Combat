using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void onPlay()
    {
        SceneManager.LoadScene(1);
        //SceneManager.LoadScene(1 - SceneManager.GetActiveScene().buildIndex);
    }

    public void onResume()
    {
        GameManager.Instance.player.GetComponent<PlayerControls>().Pause();
    }

    public void onMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void onExit()
    {
        Time.timeScale = 1.0f;
        Application.Quit();
        // o cambiar a la escena que queramos
    }
}
