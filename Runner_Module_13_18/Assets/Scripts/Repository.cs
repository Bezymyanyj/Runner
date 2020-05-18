using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Repository : SingletonAsComponent<Repository>
{
    public static Repository Instance
    {
        get { return ((Repository)_Instance); }
        set { _Instance = value; }
    }
    private string distance = "10";

    private string ballCount = "0";

    private string path = "/data.csv";

    private int index = 0;

    private string numberTitle = "Number";
    private string distanceTitle = "Distance";
    private string ballsTitle = "Balls";
    public Dictionary<int, int> Balls = new Dictionary<int, int>();
    public Dictionary<int, float> Results = new Dictionary<int, float>();
    
    public int CountBalls { get; set; }
    public float Distance { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        //Загружаем рекорды на старте
        LoadRepository();
    }
    /// <summary>
    /// Загружаем рекорды
    /// </summary>
    private void LoadRepository()
    {
        if (File.Exists(Application.dataPath + path))
        {
            //Debug.Log("Load File");
            this.Load(); // Загрузка данных
        }
        else
        {
            CreateFile(path);
            //Debug.Log("Create New File");
        }
    }

    /// <summary>
    /// Сохраняем рекорды
    /// </summary>
    public void Save()
    {
        File.Delete(Application.dataPath + path);

        for (int i = 0; i < Balls.Count; i++)
        {
            File.AppendAllText(Application.dataPath + path, $"{Results[i].ToString("#")}, {Balls[i]}\n");
        }
    }

    /// <summary>
    /// Если файла не сузествет, создаем новый
    /// Заполняем дефолтными значениями
    /// </summary>
    /// <param name="path">Путь к файлу</param>
    private void CreateFile(string path)
    {
        for (int i = 0; i < 10; i++)
        {
            File.AppendAllText(Application.dataPath + path, $"{distance}, {ballCount}\n");
        }
    }

    /// <summary>
    /// Читаем данные из файла
    /// </summary>
    private void Load()
    {
        using (StreamReader sr = new StreamReader(Application.dataPath + path))
        {
            while (!sr.EndOfStream)
            {
                //Debug.Log("Read Line Index:" + index);
                string[] args = sr.ReadLine().Split(',');
                
                Results.Add(index, float.Parse(args[0]));
                Balls.Add(index, int.Parse(args[1]));
                index++;
            }
        }
        index = 0;
    }

    
    /// <summary>
    /// Функция вывода результатов в строку
    /// </summary>
    /// <returns>Возвращаем рекорды</returns>
    public string PrintTopResults()
    {
        index++;
        string topList = "";
        string title = $"{numberTitle,5} {distanceTitle,25} {ballsTitle,10}\n";
        topList += title;
        for (int i = 0; i < Results.Count; i++)
        {
            string dictance = Results[i].ToString("#");
            topList += string.Format($"{index++,5}. {dictance,25} {Balls[i],15}\n");
        }
        index = 0;
        return topList;
        //Debug.Log(TopList.text);
    }

    /// <summary>
    /// Проверка результатов игры
    /// Если установлен новый рекорд то записываем в таблицу рекрдов.
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="balls"></param>
    public void CheckBestResult(float distance, int balls)
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
    //Debug
    private void PrintToLog()
    {
        foreach (KeyValuePair<int, int> i in Balls)
        {
            //Debug.Log(i.Value);
        }
        foreach (KeyValuePair<int, float> i in Results)
        {
            //Debug.Log(i.Value);
        }
    }
}
