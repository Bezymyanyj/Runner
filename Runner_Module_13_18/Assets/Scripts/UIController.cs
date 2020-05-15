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
    public Text TopList;
    private Repository rep;

    private bool isFall = false;
    private bool isPause = false;
    private bool isPlaying = true;
    //private string topResult;
    //private string numberTitle = "Number";
    //private string distanceTitle = "Distance";
    //private string ballsTitle = "Balls";
    //private int index = 1;
    //public Dictionary<int, int> Balls = new Dictionary<int, int>();
    //public Dictionary<int, float> Results = new Dictionary<int, float>();
    //public int CountBalls { get; set; }
    //public float Distance { get; set; }

    private void Start()
    {
        rep = GetComponent<Repository>();
    }
    private void Update()
    {
        if (isPlaying)
        {
            
            distanceCount[0].text = $"{Repository.Instance.Distance.ToString("#")}";
            distanceCount[1].text = $"Distance: {Repository.Instance.Distance.ToString("#")}";
            distanceCount[2].text = $"Distance: {Repository.Instance.Distance.ToString("#")}";
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
        rep.Save();
        Application.Quit();
    }

    public void UpdateBallText()
    {
        for (int i = 0; i < ballCount.Length; i++)
        {
            ballCount[i].text = $"Balls: {Repository.Instance.CountBalls}";
        }
    }

    public void GoToTopMenu()
    {
        fallPanel.SetActive(false);
        TopList.text = Repository.Instance.PrintTopResults();
        topPanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        fallPanel.SetActive(true);
        topPanel.SetActive(false);
    }

    public void Fall()
    {
        if (!isFall)
        {
            isFall = true;
            isPlaying = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            fallPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        rep.Save();
        SceneManager.LoadScene("Runner");
    }
}
