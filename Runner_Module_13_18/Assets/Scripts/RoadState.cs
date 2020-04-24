using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadState : MonoBehaviour
{
    public Transform roadEnd;
    // Start is called before the first frame update
    void Start()
    {
        RoadsController.Instance.buildingRoads += TryToDelAndAddRoad;
    }

    private void TryToDelAndAddRoad()
    {
        if(transform.position.z < RoadsController.Instance.minZ)
        {
            RoadsController.Instance.roadBuilder.CreateRoads();
            Destroy(gameObject);
        }
        
    }

    private void OnDestroy()
    {
        if(RoadsController.IsAlive)
            RoadsController.Instance.buildingRoads -= TryToDelAndAddRoad;
    }

    
}
