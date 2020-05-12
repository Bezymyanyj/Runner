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
    private RecordsRepository rep;

    private bool isFall = false;
    private bool isPause = false;
    private bool isPlaying = true;
    private string topResult;
    private string numberTitle = "Number";
    private string distanceTitle = "Distance";
    private string ballsTitle = "Balls";
    private int index = 1;
    public Dictionary<int, int> Balls = new Dictionary<int, int>();
    public Dictionary<int, float> Results = new Dictionary<int, float>();
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
            distanceCount[2].text = $"Distance: {Distance.ToString("#")}";
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
            ballCount[i].text = $"Balls: {CountBalls}";
        }
    }

    public void GoToTopMenu()
    {
        fallPanel.SetActive(false);
        PrintTopResults();
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
            CheckBestResult(Distance, CountBalls);
            fallPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        rep.Save();
        SceneManager.LoadScene("Runner");
    }

    private void PrintTopResults()
    {
        TopList.text = "";
        string title = $"{numberTitle,5} {distanceTitle,25} {ballsTitle,10}\n";
        TopList.text += title;
        for (int i = 0; i < Results.Count; i++)
        {
            string dictance = Results[i].ToString("#");
            TopList.text += string.Format($"{index++,5}. {dictance,25} {Balls[i],15}\n");
        }
        index = 1;
        //Debug.Log(TopList.text);
    }

    private void CheckBestResult(float distance, int balls)
    {
        //Debug.Log("How many times, I write record?");
        for (int i = 0; i < Results.Count; i++)
        {
            if (distance > Results[i])
            {
                int topTmp = Balls[i];
                Balls[i] = balls;
                balls = topTmp;
                float tmp = Results[i];
                Results[i] = distance;
                distance = tmp;
            }
        }
    }

    private void PrintToLog()
    {
        foreach(KeyValuePair<int, int> i in Balls)
        {
            Debug.Log(i.Value);
        }
        foreach(KeyValuePair<int, float > i in Results)
        {
            Debug.Log(i.Value);
        }
    }
}
