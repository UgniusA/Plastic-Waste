using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] spawnLocations;
    private GameObject globalData;

    private void Start()
    {
        globalData = GameObject.FindGameObjectWithTag("GlobalData");
    }
    private void Update()
    {
        /*if (globalData.GetComponent<GlobalData>().loading)
        {
            int spawnLocation = Mathf.RoundToInt(Random.Range(0f, 2f));
            int enemySpawn = Mathf.RoundToInt(Random.Range(0f, 1f));
            if (enemySpawn > 1)
                enemySpawn = 1;
            Instantiate(enemies[enemySpawn], spawnLocations[spawnLocation].transform.position, Quaternion.identity);
            for (var i = 0; i < spawnLocations.Length; i++)
                Destroy(spawnLocations[i]);
            Destroy(this.GetComponent<SpawnEnemies>());
        }*/
        
    }
}
