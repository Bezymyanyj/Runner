using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : SingletonAsComponent<UIController>
{
    public static UIController Instance
    {
        get { return ((UIController)_Instance); }
        set { _Instance = value; }
    }

    public GameObject pausePanel;
    public GameObject topPanel;
    public GameObject fallPanel;
    public Text[] distanceCount;
    public Text[] ballCount;
    private RecordsRepository rep;

    private bool isPause = false;
    private bool isPlaying = true;
    public string[] topList { get; set; }
    public float[] results { get; set; }
    public int CountBalls { get; set; }
    public float Distance { get; set; }

    private void Start()
    {
        rep = GetComponent<RecordsRepository>();
    }
    private void Update()
    {
        if (isPlaying)
        {
            distanceCount[0].text = $"{Distance.ToString("#")}";
            distanceCount[1].text = $"Distance: {Distance.ToString("#")}";
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPause = !isPause;
            }

            Time.timeScale = isPause ? 0 : 1;
            Cursor.lockState = isPause ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isPause ? true : false;
            pausePanel.SetActive(isPause ? true : false);
        }
    }

    public void ExitApplication()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void UpdateBallText()
    {
        for (int i = 0; i < ballCount.Length; i++)
        {
            ballCount[i].text = $"Balls: {CountBalls}";
        }
    }

    public void GoToTopMenu()
    {
        fallPanel.SetActive(false);
        topPanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        fallPanel.SetActive(true);
        topPanel.SetActive(false);
    }

    public void Fall()
    {
        isPlaying = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        fallPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Runner");
    }
}
