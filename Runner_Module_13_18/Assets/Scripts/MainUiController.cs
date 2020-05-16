using System.Collections;
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
        SoundTurnOnOff.onValueChanged.AddListener(delegate { TurnOnOffSound(SoundTurnOnOff); });
        SoundValue.onValueChanged.AddListener(delegate { ChangeSoundValue(); });
        MusicValue.onValueChanged.AddListener(delegate { ChangeMusicValue(); });
        click = GetComponent<AudioSource>();
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
        click.Play();
        SceneManager.LoadScene("Runner");
    }

    public void ExitApplication()
    {
        click.Play();
        Debug.Log("Exit");
        Application.Quit();
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
