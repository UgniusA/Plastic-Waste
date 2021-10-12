using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Variables
    [Header("Components")]

    [SerializeField]
    Animator hitBy;

    [SerializeField]
    Animator anim;

    [SerializeField]
    LayerMask toCheck;

    [Header("Int Setting")]

    [SerializeField]
    int maxHealth = 100;

    [SerializeField]
    int curHealth;

    RaycastHit hit;

    [SerializeField]
    public List<GameObject> allEnemies;

    [SerializeField]
    float minDistance;

    #endregion

    void Start()
    {
        curHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        NearbyEnemy();
        AnimationControler();
    }

    void Death()
    {
        anim.SetBool("isDying", true);

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Dying"))
        {
            anim.SetBool("isDead", true);
        }

        else if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Dead"))
        {
            anim.SetBool("isDying", false);
        }
    }

    void AnimationControler()
    {
        if (hitBy.GetCurrentAnimatorStateInfo(0).IsTag("LSlash") || hitBy.GetCurrentAnimatorStateInfo(0).IsTag("HSlash") || hitBy.GetCurrentAnimatorStateInfo(0).IsTag("KickFinish"))
        {
            if (hitBy.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.3f)
            {
                anim.SetTrigger("Hit");
            }
        }
    }

    void NearbyEnemy()
    {
        foreach(GameObject obj in allEnemies)
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            bool hasNoticedPlayer = obj.GetComponent<EnemyCombat>().isAlerted;

            if (distance <= minDistance)// && hasNoticedPlayer == true)
            {
                hitBy = obj.GetComponent<Animator>();
            }
            else if(hitBy == obj.GetComponent<Animator>() && distance > minDistance)
            {
                hitBy = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 curPos = transform.position;
        Vector3 origin = new Vector3(curPos.x, curPos.y + 1, curPos.z);

        bool isHit = Physics.SphereCast(origin, 1.3f, transform.forward, out hit, toCheck);
        if (isHit)
        {
            Debug.Log("HIT");
            Gizmos.DrawRay(origin, transform.forward * hit.distance);
        }
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            Death();
        }
    }
}
