using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplodingBall : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    private Rigidbody rb;
    private NavMeshAgent agent;

    [SerializeField]
    Transform player;

    [SerializeField]
    GameObject explosionParticle;

    [Header("Float Settings")]

    [SerializeField]
    float speed = 2;

    [SerializeField]
    float radius = 5;

    [SerializeField]
    float force = 700;

    [SerializeField]
    int attackDamage = 50;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        FindPlayer();
    }

    void FindPlayer()
    {
        float step = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, player.position) <= 1.5f)
        {
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
