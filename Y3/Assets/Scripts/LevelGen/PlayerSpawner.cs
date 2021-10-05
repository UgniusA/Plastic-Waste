using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject globalData;
    void Start()
    {
        globalData = GameObject.FindGameObjectWithTag("GlobalData");
        Instantiate(player, this.transform.position, transform.rotation);
        globalData.GetComponent<PlayerManager>().SetPlayer();
        Destroy(this);
    }
}
