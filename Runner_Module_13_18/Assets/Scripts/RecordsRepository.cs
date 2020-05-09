using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RecordsRepository : MonoBehaviour
{
    private string distance;

    private string ballCount;

    private string path = @"data.csv";

    private string[] titles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadRepository()
    {
        if (File.Exists(path))
        {
            this.Load(); // Загрузка данных
        }
        else Save(path);
    }


    public void Save(string path)
    {
        distance = $"{UIController.Instance.Distance.ToString()}";
        ballCount = $"{UIController.Instance.CountBalls.ToString()}";
        File.AppendAllText(path, $"{distance}, {ballCount}\n");
    }

    private void Load()
    {
        using (StreamReader sr = new StreamReader(this.path))
        {
            titles = sr.ReadLine().Split(',');

            for (int i = 0; i < 10; i++)
            {
                string[] args = sr.ReadLine().Split(',');

                UIController.Instance.topList[i] = $"{i}. {args[0]}, {args[1]}\n";
                UIController.Instance.results[i] = float.Parse(args[0]);
            }
        }
    }
}
