using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private BossStats bossStats;
    [SerializeField] private AudioSource audioSource;

    public GameObject weapons;
    public bool canAttack;
    public float attackCooldown;
    public bool IsAttacking = false;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && IsAttacking)
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(bossStats.damage);
        }
    }
    public void EnemyAttacking()
    {
        if (canAttack) 
        { 
            IsAttacking = true;
            canAttack = false;
            SoundManager.Instance.Play(audioSource, SoundManager.Sound.EnemyAttack);
            StartCoroutine(EnemyResetAttackCooldown());
        }
    }

    IEnumerator EnemyResetAttackCooldown()
    {
        StartCoroutine(EnemyResetAttackBool());
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator EnemyResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        IsAttacking = false;
    }
}
