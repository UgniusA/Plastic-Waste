using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    [Header("Cone of Vision Settings")]

    [SerializeField]
    float visionRange;

    [SerializeField]
    float visionConeRange;

    [SerializeField]
    bool isAlerted;

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
    float attackRate = 2f;

    [SerializeField]
    float attackRange = 0.5f;

    [SerializeField]
    bool damageDealt;
    #endregion

    public float speed = 2;
    private NavMeshAgent agent;


    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        Offset();
    }

    void Update()
    {
        //ComboTest();
        AttackRate();
        ControlsTest();

        SpottedPLayer();
        LookAtPlayer();

        //MoveToPlayer();

        //Damaged();
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

    void AttackRate()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                //anim.speed = 1f;
                anim.SetTrigger("LSlash");
                anim.SetTrigger("Combo");
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                //anim.speed = 0.5f;
                anim.SetTrigger("HSlash");
                //WaitToAttack();
            }
        }
    }

    public void Light()
    {
        player.GetComponent<Health>().TakeDamage(lightDamage);
    }

    public void Heavy()
    {
        player.GetComponent<Health>().TakeDamage(lightDamage * 2);
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
        Vector3 offsett = new Vector3(playerPos.x + 2, playerPos.y, playerPos.z);
        float step = speed * Time.deltaTime; // calculate distance to move

        if (Vector3.Distance(transform.position, player.transform.position) <= 2)
        {
            Debug.Log("Found");
            anim.SetBool("Running", false);
        }
        else //if(isAlerted)
        {
            //agent.destination = player.transform.position;
            agent.destination = offsett;
            anim.SetBool("Running", true);
            //transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            //rb.AddForce(transform.forward * speed);
        }
    }

    void Offset()
    {
        var playerPos = player.transform.position;
        Vector3 dir = (player.transform.position - transform.position).normalized;
        Vector3 offsett = new Vector3(playerPos.x + dir.x, playerPos.y, playerPos.z);

        //Debug.DrawLine(transform.position, transform.position + dir * 10, Color.red, Mathf.Infinity);
        transform.position = transform.position + dir * 10;
        //Instantiate(gameObject, transform.position + dir * 10, Quaternion.identity);
    }

    #region To delete
    private IEnumerator WaitForAnimation(Animation animation)
    {
        do
        {
            yield return null;
        } while (animation.isPlaying);
    }

    void Damaged()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("LSlash"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime == 0.3f)
            {
                player.GetComponent<Health>().TakeDamage(lightDamage);
            }
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("HSlash"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime == 0.3f)
            {
                player.GetComponent<Health>().TakeDamage(heavyDamage);
            }
        }
    }

    void ComboTest()
    {
        var toDamage = player.GetComponent<Health>();

        if (!damageDealt)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Light Slash"))
            {
                toDamage.TakeDamage(lightDamage);
                damageDealt = true;
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy Slash"))
            {
                toDamage.TakeDamage(lightDamage * 2);
                damageDealt = true;
            }
        }

        else
        {
            //damageDealt = false;
        }




        /*        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Light Slash"))
                {
                    Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

                    foreach (Collider player in hits)
                    {
                        player.GetComponent<Health>().TakeDamage(lightDamage);
                    }
                }

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy Slash"))
                {
                    Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

                    foreach (Collider player in hits)
                    {
                        player.GetComponent<Health>().TakeDamage(heavyDamage);
                        //yield WaitForSeconds(AnimatorClipInfo["Die"].length)
                    }
                }*/
    }

    void WaitToAttack()
    {
        nextAttackTime = Time.time + 3f / attackRate;
    }

    void LightSlash()
    {
        anim.SetTrigger("LSlash");

        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider player in hits)
        {
            //player.GetComponent<Health>().TakeDamage(lightDamage);
        }
    }

    void HeavySlash()
    {
        anim.SetTrigger("HSlash");

        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider player in hits)
        {
            //player.GetComponent<Health>().TakeDamage(heavyDamage);
        }
    }
    #endregion
}
