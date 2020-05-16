using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioController : SingletonAsComponent<AudioController>
{
    //В этом классе хранятся данные о настройке звука и эти данные передаются между сценами
    public static AudioController Instance
    {
        get { return ((AudioController)_Instance); }
        set { _Instance = value; }
    }

    private bool soundOnOff = true;
    private float soundValue = 0f;
    private float musicValue = 0f;

    #region //Получаем значение
    public void SoundOnOff( bool turn)
    {
        soundOnOff = turn;
    }

    public void SoundValue(float value)
    {
        soundValue = value;
    }
    public void MusicValue(float value)
    {
        musicValue = value;
    }
    #endregion

    #region    // Отправляем значение
    public bool GetSoundOnOff()
    {
        return soundOnOff;
    }

    public float GetSoundValue()
    {
        return soundValue;
    }

    public float GetMusicValue()
    {
        return musicValue;
    }
    #endregion
}
