using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public AudioMixer mixer;

    public AudioClip[] clips;

    //Скорость плавного перехода между треками
    public float crossFadeRate = 1.5f;

    //Первый источник музыки
    public AudioSource musicFirstSource;
    //Второй источник музыки
    public AudioSource musicSecondSource;

    private AudioSource firstMusic;
    private AudioSource secondMusic;

    private int currentClipNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        musicFirstSource.ignoreListenerVolume = true;
        musicFirstSource.ignoreListenerPause = true;
        musicSecondSource.ignoreListenerVolume = true;
        musicSecondSource.ignoreListenerPause = true;

        if (clips.Length != 0)
            musicFirstSource.clip = clips[0];

        firstMusic = musicFirstSource;
        secondMusic = musicSecondSource;
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstMusic.isPlaying && !secondMusic.isPlaying)
        {
            NextMusic();
            //Debug.Log("Next Track");
        }
    }
    /// <summary>
    /// Переключаем треки
    /// </summary>
    private void NextMusic()
    {
        if (clips.Length == 0)
        {
            //Debug.Log("Clips Empty");
            return;
        }

        if (currentClipNumber >= clips.Length - 1)
            currentClipNumber = 0;
        else
            currentClipNumber++;

        firstMusic.clip = secondMusic.clip;
        secondMusic.clip = clips[currentClipNumber];

        musicFirstSource.Play();
    }
}
