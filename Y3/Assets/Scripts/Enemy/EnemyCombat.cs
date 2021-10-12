using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyCombat : MonoBehaviour
{
    #region Variables

    [Header("Object Settings")]

    [SerializeField]
    Animator anim;

    [SerializeField]
    Transform attackPoint;

    [SerializeField]
    GameObject player;

    [SerializeField]
    NavMeshAgent agent;

    [Header("Cone of Vision Settings")]

    [SerializeField]
    float visionRange;

    [SerializeField]
    float visionConeRange;

    [SerializeField]
    public bool isAlerted;

    [SerializeField]
    LayerMask playerLayer;

    [Header("Attack Settings")]

    [SerializeField]
    float rotationSpped = 2;

    [SerializeField]
    float nextAttackTime = 0f;

    [SerializeField]
    public int lightDamage = 20;

    [SerializeField]
    public int heavyDamage = 40;

    [SerializeField]
    public int kickDamage = 40;

    [SerializeField]
    float attackRate = 2f;

    [SerializeField]
    float attackRange = 0.5f;

    [SerializeField]
    bool damageDealt;

    [Header("Patrol Settings")]

    [SerializeField]
    Transform[] patrolRoute;

    [SerializeField]
    int curPatrolPos = 0;
    #endregion

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player.GetComponent<Health>().allEnemies.Add(gameObject);
    }

    void Update()
    {
        AttackRate();
        BlockTest();
        SpottedPLayer();
        LookAtPlayer();
        Patrol();

        MoveToPlayer();
    }
    void AttackRate()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                //anim.speed = 1f;
                anim.SetTrigger("LSlash");//Quick Slash
                anim.SetTrigger("Combo1");//Heavy Swing
                anim.SetTrigger("Combo2");//Kick Finish
            }
        }
    }

    void BlockTest()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Block();
        }
        else
        {
            anim.SetBool("Blocking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Random.value > 0.5f)//50% chnace to block
            {
                anim.SetBool("Blocking", true);
            }
            else
            {
                anim.SetTrigger("Hit");
            }
        }
    }

    void SpottedPLayer()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 vectorToPlayer = playerPos - transform.position;

        if (Vector3.Distance(transform.position, playerPos) <= visionRange)
        {
            if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeRange)
            {
                isAlerted = true;
                //Debug.Log("Spotted");
            }
        }
        else
        {
            isAlerted = false;
            //Debug.Log("Hidden");
        }
    }

    void MoveToPlayer()
    {
        var playerPos = player.transform.position;
        Vector3 offsett = new Vector3(playerPos.x + 1, playerPos.y, playerPos.z);

        if (Vector3.Distance(transform.position, player.transform.position) <= 2)
        {
            //Debug.Log("Found");
            anim.SetBool("Running", false);
        }
        else if(isAlerted)
        {
            agent.destination = offsett;
            anim.SetBool("Running", true);
        }
    }

    void LookAtPlayer()
    {
        Vector3 targetDirection = player.transform.position - transform.position;
        float rotSpeed = rotationSpped * Time.deltaTime;
        Vector3 lookAt = Vector3.RotateTowards(transform.forward, targetDirection, rotSpeed, 0.0f);

        if (isAlerted)
        {
            transform.rotation = Quaternion.LookRotation(lookAt);
        }
    }

    void Patrol()
    {
        Vector3 comparePos = new Vector3(patrolRoute[curPatrolPos].position.x, transform.position.y, patrolRoute[curPatrolPos].position.z);

        agent.destination = patrolRoute[curPatrolPos].position;

        if (!isAlerted)
        {
            anim.SetBool("Walking", true);

            if (transform.position == comparePos && !isAlerted)
            {
                curPatrolPos = (curPatrolPos + 1) % patrolRoute.Length;
            }
        }

    }

    #region Combat Numbers
    public void Light()
    {
        player.GetComponent<Health>().TakeDamage(lightDamage);
    }

    public void Heavy()
    {
        player.GetComponent<Health>().TakeDamage(heavyDamage);
    }

    public void Kick()
    {
        player.GetComponent<Health>().TakeDamage(kickDamage);
    }

    void Block()
    {
        float chanceToBlock = Random.Range(0, 1);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
