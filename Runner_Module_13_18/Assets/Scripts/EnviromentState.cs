using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentState : MonoBehaviour
{
    public Transform enviromentEnd;
    // Start is called before the first frame update
    void Start()
    {
        RoadsController.Instance.buildingEnviroment += TryToDellAndAddEnviroment;
    }

    private void TryToDellAndAddEnviroment()
    {
        if(transform.position.z < RoadsController.Instance.minZ)
        {
            RoadsController.Instance.roadBuilder.CreateEnviroments();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (RoadsController.IsAlive)
            RoadsController.Instance.buildingEnviroment -= TryToDellAndAddEnviroment;
    }
}


