using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadsController : SingletonAsComponent<RoadsController>
{
    public static RoadsController Instance
    {
        get { return ((RoadsController)_Instance); }
        set { _Instance = value; }
    }

    public float minZ = -20;
    public float speed = 2f;
    public float speedTimeChanging = 50;
    public float speedValueChanging = 1f;
    public int freeRoadsRatio = 30;
    public int hardObstacleRoadsRatio = 90;
    public int speedChangingRoads = 3;

    public RoadBuilder roadBuilder;


    public static bool isFall = false;

    public delegate void TryToDelAndAddRoad();
    public event TryToDelAndAddRoad buildingRoads;

    public delegate void TryToDelAndAddInviroment();
    public event TryToDelAndAddInviroment buildingEnviroment;

   

    private int index = 0;
    void Start()
    {
        StartCoroutine(AddAndDelRoads());
        StartCoroutine(ChangeSpeed());

        //Устонавливаем коэффициенты частоты появления дорог  
        roadBuilder.FreeRoadsRatio = freeRoadsRatio;
        roadBuilder.HardObstacleRoadsRatio = hardObstacleRoadsRatio;

        isFall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFall)
            transform.position -= Vector3.forward * speed * Time.deltaTime;
    }

    private IEnumerator AddAndDelRoads()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (buildingRoads != null)
            {
                buildingRoads();
            }
            if(buildingEnviroment != null)
            {
                buildingEnviroment();
            }
        }
    }

    private IEnumerator ChangeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedTimeChanging);
            if(!isFall)
                speed += speedValueChanging;
            index++;
            // меням коэффиценты с некоторой переодичностью,
            // простые платформы появляются реже, а более сложные чаще
            if(index == speedChangingRoads && roadBuilder.FreeRoadsRatio != 0)
            {
                roadBuilder.FreeRoadsRatio--;
                roadBuilder.HardObstacleRoadsRatio--;
                index = 0;
            }
        }
    }
}
