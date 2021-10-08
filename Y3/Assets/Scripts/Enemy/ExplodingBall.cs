using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplodingBall : MonoBehaviour
{
    public float speed = 2;
    private Rigidbody rb;

    public float radius = 5;
    public float force = 700;


    public Transform player;

    bool hasExploded;

    public int attackDamage = 50;

    private NavMeshAgent agent;

    public GameObject explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

/*    private void FixedUpdate()
    {
        float horzMove = Input.GetAxis("Horizontal");
        float vertMove = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horzMove, 0.0f, vertMove);

        rb.AddForce(movement * speed);

    }*/
    // Update is called once per frame
    void Update()
    {
        FindPlayer();
    }

    void FindPlayer()
    {
        float step = speed * Time.deltaTime; // calculate distance to move

        if (Vector3.Distance(transform.position, player.position) <= 1.5f)
        {
            //Debug.Log("Found");
            Explode();
        }
        else
        {
            agent.destination = player.position;

            //transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            //rb.AddForce(transform.forward * speed);
        }
    }
    void Explode()
    {
        Instantiate(explosionParticle, transform.position, transform.rotation);

        Collider[] col = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObj in col)
        {
            Rigidbody rb = nearbyObj.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

            if(nearbyObj.name == "Bot")
            {
                nearbyObj.GetComponent<Health>().TakeDamage(attackDamage);
                //Debug.Log("Player");
            }
        }
        Destroy(gameObject);
    }
}
