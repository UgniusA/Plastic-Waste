using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyombatTest : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //AttackCombo();
        anim.SetTrigger("LSlash");
        anim.SetTrigger("Combo");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AttackCombo()
    {
        anim.SetTrigger("LSlash");

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Light Slash")) // check if "bash" is playing...
        {
            Debug.Log("Done");
            //anim.SetTrigger("HSlash");
        }
    }
}
