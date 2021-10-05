using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.AI;

public class RoomController : MonoBehaviour
{
    public GameObject[] allRooms;
    public GameObject[] connectors;
    public GameObject[] bossRooms;
    public int segmentsToBeMade;
    public int roomsToBeMade;
    public int leftTurns;
    public int rightTurns;
    public int roomsCompleted;
    public bool start;
    public bool restart;
    void Awake()
    {
        roomsToBeMade = Mathf.RoundToInt(UnityEngine.Random.Range(3f, 8f));


    }

    // Update is called once per frame
    void Update()
    {
        if(segmentsToBeMade <= 0)
        {
            segmentsToBeMade = Mathf.RoundToInt(UnityEngine.Random.Range(5f, 7f));
        }
        
    }
}
