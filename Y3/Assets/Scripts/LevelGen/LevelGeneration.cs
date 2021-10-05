using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public bool start = false;
    private int roomNumber;
    private int segmentsLeft;
    private GameObject room;
    private GameObject roomController;
    private GameObject globalData;

    // Start is called before the first frame update
    void Start()
    {
        globalData = GameObject.FindGameObjectWithTag("GlobalData");
        roomController = GameObject.FindWithTag("RoomController");
    }

    // Update is called once per frame
    void Update()
    {
        segmentsLeft = roomController.GetComponent<RoomController>().segmentsToBeMade;
        if (start == true)
        {
            roomNumber = Mathf.RoundToInt(Random.Range(11f, 12f));
        }
        else
        {
            if (segmentsLeft == 1)
            {
                roomNumber = Mathf.RoundToInt(Random.Range(13f, 14f));
            }
            else
            {
                roomNumber = Mathf.RoundToInt(Random.Range(0f, 10f));
            }
        }
        room = roomController.GetComponent<RoomController>().allRooms[roomNumber];
        
        
        Instantiate(room, this.transform.position, transform.rotation);
        globalData.GetComponent<GlobalData>().roomsSpawned.Add(room);
        roomController.GetComponent<RoomController>().segmentsToBeMade--;
        Destroy(this);

    }
}
