using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RecordsRepository : MonoBehaviour
{
    private string distance = "10";

    private string ballCount = "0";

    private string path = "/data.csv";

    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        LoadRepository();
    }

    private void LoadRepository()
    {
        if (File.Exists(Application.dataPath + path))
        {
            Debug.Log("Load File");
            this.Load(); // Загрузка данных
        }
        else
        {
            CreateFile(path);
            Debug.Log("Create New File");
        }
    }

    public void Save()
    {
        File.Delete(Application.dataPath + path);

        for (int i = 0; i < UIController.Instance.Balls.Count; i++)
        {
            File.AppendAllText(Application.dataPath + path, $"{UIController.Instance.Results[i].ToString("#")}, {UIController.Instance.Balls[i]}\n");
        }
    }


    private void CreateFile(string path)
    {
        for (int i = 0; i < 10; i++)
        {
            File.AppendAllText(Application.dataPath + path, $"{distance}, {ballCount}\n");
        }
    }

    private void Load()
    {
        using (StreamReader sr = new StreamReader(Application.dataPath + path))
        {
            while (!sr.EndOfStream)
            {
                //Debug.Log("Read Line Index:" + index);
                string[] args = sr.ReadLine().Split(',');
                
                UIController.Instance.Results.Add(index, float.Parse(args[0]));
                UIController.Instance.Balls.Add(index, int.Parse(args[1]));
                index++;
            }
        }
        index = 0;
    }
}
