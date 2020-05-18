using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    private string pathSettings = "/settings.csv";

    public Dictionary<string, float> Settings = new Dictionary<string, float>()
    {
        { "Sound", 1 },
        { "SoundValue", 0 },
        { "MusicValue", 0 }
    };

    private bool soundOnOff = true;

    private void Awake()
    {
        LoadSettingsRep();
        SetSettings();
    }

    /// <summary>
    /// Загружаем Настройки звука
    /// </summary>
    private void LoadSettingsRep()
    {
        if (File.Exists(Application.dataPath + pathSettings))
        {
            this.LoadSetting();
        }
        else
        {
            CreateFileSettings();
        }
    }

    /// <summary>
    /// Сохраняем настройки звука
    /// </summary>
    public void SaveFileSettings()
    {
        File.Delete(Application.dataPath + pathSettings);

        foreach (var item in Settings)
        {
            File.AppendAllText(Application.dataPath + pathSettings, $"{item.Key}, {item.Value.ToString("#")}\n");
        }
    }
    /// <summary>
    /// Создаем файл с настройками звука
    /// </summary>
    private void CreateFileSettings()
    {
        File.AppendAllText(Application.dataPath + pathSettings, $"Sound, 1\n");
        File.AppendAllText(Application.dataPath + pathSettings, $"SoundValue, 0\n");
        File.AppendAllText(Application.dataPath + pathSettings, $"MusicValue, 0\n");
    }

    /// <summary>
    /// Читаем настройки звука и записываем в словарь
    /// </summary>
    private void LoadSetting()
    {
        using (StreamReader sr = new StreamReader(Application.dataPath + pathSettings))
        {
            while (!sr.EndOfStream)
            {
                string[] args = sr.ReadLine().Split(',');

                Settings[args[0]] = float.Parse(args[1]);
            }
        }
    }
    /// <summary>
    /// Переводим значение включен звук или нет из float to bool
    /// </summary>
    private void SetSettings()
    {
        if (Settings["Sound"] < 1)
        {
            soundOnOff = false;
        }
        else
        {
            soundOnOff = true;
        }
        SetSoundOnOff(soundOnOff);
    }
    #region //Получаем значение
    public void SetSoundOnOff( bool turn)
    {
        soundOnOff = turn;
        if (turn)
        {
            Settings["Sound"] = 1;
        }
        else
        {
            Settings["Sound"] = 0;
        }
        
    }
    #endregion

    #region    // Отправляем значение
    public bool GetSoundOnOff()
    {
        return soundOnOff;
    }
    #endregion

    //Debug
    private void PrintToLog()
    {
        foreach (KeyValuePair<string, float> i in Settings)
        {
            Debug.Log($"{i.Key} {i.Value}");
        }
    }
}
