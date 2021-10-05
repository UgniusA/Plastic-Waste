using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AddConnector : MonoBehaviour
{
    private int connectorNumber;
    private GameObject globalData;
    private GameObject roomController;
    private GameObject connector;
    private GameObject bossRoom;
    public NavMeshData navmesh;
    public NavMeshSurface[] surfaces;

    private void Start()
    {
        globalData = GameObject.FindGameObjectWithTag("GlobalData");
        roomController = GameObject.FindWithTag("RoomController");
        if(roomController.GetComponent<RoomController>().roomsToBeMade > 0)
        {
            connectorNumber = Mathf.RoundToInt(Random.Range(0f, 6f));
            connector = roomController.GetComponent<RoomController>().connectors[connectorNumber];
            globalData.GetComponent<GlobalData>().roomsSpawned.Add(connector);
            Instantiate(connector, this.transform.position, transform.rotation);
            roomController.GetComponent<RoomController>().roomsToBeMade--;
            roomController.GetComponent<RoomController>().roomsCompleted++;
            Destroy(this);
        }
        else
        {
            bossRoom = roomController.GetComponent<RoomController>().bossRooms[0];
            Instantiate(bossRoom, this.transform.position, transform.rotation);
            globalData.GetComponent<GlobalData>().roomsSpawned.Add(bossRoom);
            for (int i = 0; i < surfaces.Length; i++)
            {
                surfaces[0].BuildNavMesh();
            }
            globalData.GetComponent<GlobalData>().loading = true;
            Destroy(this);
        }
    }
    
}
