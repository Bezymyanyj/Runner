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
        // Подписываемся на тогле и слайдеры
        SoundTurnOnOff.onValueChanged.AddListener(delegate { TurnOnOffSound(SoundTurnOnOff); });
        SoundValue.onValueChanged.AddListener(delegate { ChangeSoundValue(); });
        MusicValue.onValueChanged.AddListener(delegate { ChangeMusicValue(); });
        SetUI();
    }
    private void Update()
    {
        if (isPlaying)
        {
            
            distanceCount[0].text = $"{Repository.Instance.Distance.ToString("#")}";
            distanceCount[1].text = $"Distance: {Repository.Instance.Distance.ToString("#")}";
            distanceCount[2].text = $"Distance: {Repository.Instance.Distance.ToString("#")}";

            //Вызов меню паузы
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPause)
                {
                    PauseGame();
                }
                else
                {
                    UnPauseGame();
                }
            }
        }
    }

    public void PauseGame()
    {
        isPause = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pausePanel.SetActive(true);
    }

    public void UnPauseGame()
    {
        isPause = false;
        AudioController.Instance.SaveFileSettings();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausePanel.SetActive(false);
    }

    public void ExitApplication()
    {
        AudioController.Instance.SaveFileSettings();
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
        AudioController.Instance.SaveFileSettings();
        click.Play();
        Repository.Instance.Save();
        SceneManager.LoadScene("Runner");
    }
    /// <summary>
    /// Проигрываем звук на слайдере громкости звуков
    /// </summary>
    public void PlayClick()
    {
        if (!click.isPlaying)
            click.Play();
    }
    /// <summary>
    /// Устанавливаем значения на UI элементах
    /// </summary>
    private void SetUI()
    {
        SoundTurnOnOff.isOn = AudioController.Instance.GetSoundOnOff();
        SoundValue.SetValueWithoutNotify(AudioController.Instance.Settings["SoundValue"]);
        MusicValue.SetValueWithoutNotify(AudioController.Instance.Settings["MusicValue"]);
    }
    #region // Устанавливаем громкость звука
    private void TurnOnOffSound(Toggle change)
    {
        AudioController.Instance.SetSoundOnOff(change.isOn);
        mixerController.SoundOnOff(change.isOn);
    }

    private void ChangeSoundValue()
    {
        AudioController.Instance.Settings["SoundValue"] = SoundValue.value;
        mixerController.SoundValue(SoundValue.value);
    }

    private void ChangeMusicValue()
    {
        AudioController.Instance.Settings["MusicValue"] = MusicValue.value;
        mixerController.MusicValue(MusicValue.value);
    }
    #endregion
}
