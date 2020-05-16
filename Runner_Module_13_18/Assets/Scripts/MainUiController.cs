﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUiController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject topPanel;
    public MixerController mixerController;

    public Text TopList;
    public Toggle SoundTurnOnOff;
    public Slider SoundValue;
    public Slider MusicValue;

    private AudioSource click;
    private void Start()
    {
        click = GetComponent<AudioSource>();
        // Подписываемся на тогле и слайдеры
        SoundTurnOnOff.onValueChanged.AddListener(delegate { TurnOnOffSound(SoundTurnOnOff); });
        SoundValue.onValueChanged.AddListener(delegate { ChangeSoundValue(); });
        MusicValue.onValueChanged.AddListener(delegate { ChangeMusicValue(); });
        Invoke("SetUI", 0.2f);
    }

    public void GoToTopMenu()
    {
        mainPanel.SetActive(false);
        click.Play();
        TopList.text = Repository.Instance.PrintTopResults();
        topPanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        click.Play();
        mainPanel.SetActive(true);
        topPanel.SetActive(false);
    }

    public void StartGame()
    {
        AudioController.Instance.SaveFileSettings();
        click.Play();
        SceneManager.LoadScene("Runner");
    }

    public void ExitApplication()
    {
        AudioController.Instance.SaveFileSettings();
        click.Play();
        Debug.Log("Exit");
        Application.Quit();
    }

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
