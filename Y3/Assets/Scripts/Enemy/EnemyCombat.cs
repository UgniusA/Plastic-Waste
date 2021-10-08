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


    public int lightDamage = 20;
    public int heavyDamage = 80;


    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Start()
    {
        //AttackCombo();
        //anim.SetTrigger("LSlash");
        //anim.SetTrigger("Combo");
        //LightSlash();
    }
    void Update()
    {
        ComboTest();
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
                anim.SetTrigger("LSlash");
                anim.SetTrigger("Combo");
/*                LightSlash();
                WaitToAttack();*/
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                HeavySlash();
                WaitToAttack();
            }
        }
    }

    void ComboTest()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Light Slash"))
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
        }
    }

    private IEnumerator WaitForAnimation(Animation animation)
    {
        do
        {
            yield return null;
        } while (animation.isPlaying);
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
            player.GetComponent<Health>().TakeDamage(lightDamage);
        }
    }

    void HeavySlash()
    {
        anim.SetTrigger("HSlash");

        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        foreach (Collider player in hits)
        {
            player.GetComponent<Health>().TakeDamage(heavyDamage);
        }
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
