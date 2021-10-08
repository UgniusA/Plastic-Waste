using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;

    [SerializeField]
    public Animator hitBy;

    AnimatorClipInfo[] m_CurrentClipInfo;
    Animation curAnim;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    void Update()
    {
        AnimationTrigger();
    }
    void Death()
    {
        Debug.Log("Dead");
    }

    void AnimationTrigger()
    {
        if (hitBy.GetCurrentAnimatorStateInfo(0).IsTag("LSlash") || hitBy.GetCurrentAnimatorStateInfo(0).IsTag("HSlash"))
        {
            if (hitBy.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.3f)
            {
                GetComponent<Animator>().SetTrigger("Hit");
            }
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
