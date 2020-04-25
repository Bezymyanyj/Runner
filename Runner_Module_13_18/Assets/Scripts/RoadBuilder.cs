using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBuilder : MonoBehaviour
{
    public GameObject[] freeRoads;
    public GameObject[] obstacleRoads;
    public GameObject[] hardObstacleRoads;
    public GameObject[] enviroment;
    public Transform roadContainer;

    public int FreeRoadsRatio { get; set; }
    public int HardObstacleRoadsRatio { get; set; }

    private Transform lastRoad = null;
    private Transform lastEnviroment = null;

    private bool wasFreeCreate = false;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    private void Init()
    {
        CreateFreeRoadByIndex(0);
        for (int i = 0; i < 5; i++)
        {
            CreateEnviroment();
            CreateRoads();
        }
    }
    public void CreateRoads()
    {
        int weight = Random.Range(1, 101);
        if (weight < FreeRoadsRatio && !wasFreeCreate)
        {
            CreateFreeRoad();
            wasFreeCreate = true;
        }
        else if(weight > HardObstacleRoadsRatio)
        {
            CreateHardObstacleRoad();
            wasFreeCreate = false;
        }
        else
        {
            CreateObstacleRoad();
            wasFreeCreate = false;
        }
    }

    public void CreateEnviroments()
    {
        CreateEnviroment();
    }

    private void CreateFreeRoad()
    {
        Vector3 possition = (lastRoad == null) ? 
            roadContainer.position : lastRoad.GetComponent<RoadState>().roadEnd.position;

        int index = Random.Range(0, freeRoads.Length);
        GameObject road = Instantiate(freeRoads[index], possition, Quaternion.identity, roadContainer);
        lastRoad = road.transform;
    }
    private void CreateFreeRoadByIndex(int index)
    {
        Vector3 possition = (lastRoad == null) ?
            roadContainer.position : lastRoad.GetComponent<RoadState>().roadEnd.position;

        GameObject road = Instantiate(freeRoads[index], possition, Quaternion.identity, roadContainer);
        lastRoad = road.transform;
    }

    private void CreateObstacleRoad()
    {
        Vector3 possition = (lastRoad == null) ?
            roadContainer.position : lastRoad.GetComponent<RoadState>().roadEnd.position;

        int index = Random.Range(0, obstacleRoads.Length);
        GameObject road = Instantiate(obstacleRoads[index], possition, Quaternion.identity, roadContainer);
        lastRoad = road.transform;
    }

    private void CreateHardObstacleRoad()
    {
        Vector3 possition = (lastRoad == null) ?
            roadContainer.position : lastRoad.GetComponent<RoadState>().roadEnd.position;

        int index = Random.Range(0, hardObstacleRoads.Length);
        GameObject road = Instantiate(hardObstacleRoads[index], possition, Quaternion.identity, roadContainer);
        lastRoad = road.transform;
    }

    private void CreateEnviroment()
    {
        Vector3 possition = (lastEnviroment == null) ?
            roadContainer.position : lastEnviroment.GetComponent<EnviromentState>().enviromentEnd.position;

        int index = Random.Range(0, enviroment.Length);
        GameObject road = Instantiate(enviroment[index], possition, Quaternion.identity, roadContainer);
        lastEnviroment = road.transform;
    }
}
