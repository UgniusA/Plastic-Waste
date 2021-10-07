using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator anim;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public GameObject player;

    [SerializeField]
    float visionRange;

    [SerializeField]
    float visionConeRange;

    [SerializeField]
    bool isAlerted;


    public int attackDamage = 50;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    void Update()
    {
        AttackRate();
        ControlsTest();

        SpottedPLayer();
    }

    void ControlsTest()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Block();
        }
        else
        {
            anim.SetBool("Blocking", false);
            //Debug.Log("Stop");
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

    void AttackRate()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                LightSlash();
                WaitToAttack();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                HeavySlash();
                WaitToAttack();
            }
        }
    }

    void WaitToAttack()
    {
        nextAttackTime = Time.time + 3f / attackRate;
    }

    void LightSlash()
    {
        anim.SetTrigger("LSlash");

        Collider[] hits = Physics.OverlapSphere(attackPoint.position,attackRange,playerLayer);

        foreach(Collider player in hits)
        {
            //Destroy(player.gameObject);
            player.GetComponent<Health>().TakeDamage(attackDamage);
            //player.GetComponent<Health>().hitBy = gameObject.GetComponent<Animator>();
        }
        //Debug.Log("Light");
    }

    void HeavySlash()
    {
        anim.SetTrigger("HSlash");
        //Debug.Log("Heavy");
    }

    void Block()
    {
        var chanceToBlock = Random.Range(0, 1);
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
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
                Debug.Log("Spotted");
            }
        }
        else
        {
            isAlerted = false;
            Debug.Log("Hidden");
        }
    }
}
