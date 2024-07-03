using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public GameObject menu;
    public GameObject controls;

    public void onPlay()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
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

    public void onControlsMenu()
    {
        menu.SetActive(false);
        controls.SetActive(true);
    }

    public void onMMenu()
    {
        controls.SetActive(false);
        menu.SetActive(true);
    }


    public void onExit()
    {
        //Time.timeScale = 1.0f;
        Application.Quit();
        // o cambiar a la escena que queramos
    }
}
