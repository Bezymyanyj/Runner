using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUiController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject topPanel;


    public void GoToTopMenu()
    {
        mainPanel.SetActive(false);
        topPanel.SetActive(true);
    }

    public void GoToMainMenu()
    {

        mainPanel.SetActive(true);
        topPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Runner");
    }

    public void ExitApplication()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
