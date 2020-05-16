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

    public MixerController mixerController;
    public GameObject pausePanel;
    public GameObject topPanel;
    public GameObject fallPanel;
    public Text[] distanceCount;
    public Text[] ballCount;
    public Text TopList;
    public Toggle SoundTurnOnOff;
    public Slider SoundValue;
    public Slider MusicValue;

    private AudioSource click;
    private bool isFall = false;
    private bool isPause = false;
    private bool isPlaying = true;
    private void Start()
    {
        click = GetComponent<AudioSource>();
        SoundTurnOnOff.onValueChanged.AddListener(delegate { TurnOnOffSound(SoundTurnOnOff); });
        SoundValue.onValueChanged.AddListener(delegate { ChangeSoundValue(); });
        MusicValue.onValueChanged.AddListener(delegate { ChangeMusicValue(); });
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
        click.Play();
        Debug.Log("Exit");
        Repository.Instance.Save();
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
        click.Play();
        fallPanel.SetActive(false);
        TopList.text = Repository.Instance.PrintTopResults();
        topPanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        click.Play();
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
        click.Play();
        Repository.Instance.Save();
        SceneManager.LoadScene("Runner");
    }

    private void TurnOnOffSound(Toggle change)
    {
        AudioController.Instance.SoundOnOff(change.isOn);
        mixerController.SoundOnOff(change.isOn);
    }

    private void ChangeSoundValue()
    {
        AudioController.Instance.SoundValue(SoundValue.value);
        mixerController.SoundValue(SoundValue.value);
    }

    private void ChangeMusicValue()
    {
        AudioController.Instance.MusicValue(MusicValue.value);
        mixerController.MusicValue(MusicValue.value);
    }
}
