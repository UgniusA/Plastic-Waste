using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitTest : MonoBehaviour
{
    //public GameObject sword;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (sword.GetComponent<MeshCollider>().

    }

/*    void OnCollisionEnter(Collision collision)
    {
        //Destroy(collision.gameObject);
        Debug.Log("HIT");
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT");
    }

    /*   private void OnTriggerEnter(Collider other)
       {
           Debug.Log("HIT");
       }*/
}
