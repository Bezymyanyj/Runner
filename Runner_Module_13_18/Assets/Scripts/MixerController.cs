using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    public AudioMixer Mixer;


    private void Start()
    {
        SoundOnOff(AudioController.Instance.GetSoundOnOff());
        SoundValue(AudioController.Instance.GetSoundValue());
        MusicValue(AudioController.Instance.GetMusicValue());
    }
    public void SoundOnOff(bool turn)
    {
        if (turn)
            Mixer.SetFloat("Sound", 0);
        else
            Mixer.SetFloat("Sound", -80);
    }

    public void SoundValue(float value)
    {
        Mixer.SetFloat("SoundValue", value);
    }
    public void MusicValue(float value)
    {
        Mixer.SetFloat("MusicValue", value);
    }
}
