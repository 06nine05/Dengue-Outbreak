using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject weapons;
    public bool canAttack;
    public float attackCooldown;
    public bool IsAttacking = false;
    
    public void ButtonAttack()
    {
        if (canAttack)
        {
            WeaponsAttack();
        }
    }

    public void WeaponsAttack()
    {
        IsAttacking = true; 
        canAttack = false;
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        IsAttacking = false;
    }
}
