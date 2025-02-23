using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Buttons : MonoBehaviour
{
    public Button Play;
    public Button Setting;
    public Button Quit;

    public GameObject settingPanel;
    public GameObject PauseUI;
    public GameObject ControlPanel;

    private void Start()
    {
        if (settingPanel == null)
        {
            Debug.Log("Insert Setting Panel");
        }
        if (PauseUI == null)
        {
            Debug.Log("Insert PauseUI");
        }

        if (ControlPanel == null)
        {
            Debug.Log("Insert ControlPanel");
        }
    }
    public void OnPlay()
    {
        SceneManager.LoadScene(1);
        Debug.Log("GameOn");
    }

    public void OnSetting()
    {
        settingPanel.SetActive(true);
        Debug.Log("Setting");
    }

    public void OnQuit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void OnPause()
    {
        PauseUI.SetActive(true);
        ControlPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void OnResume()
    {
        PauseUI.SetActive(false);
        ControlPanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void OnMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("menu");
    }
}
