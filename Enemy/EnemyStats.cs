using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : CharacterStats
{
    [SerializeField] private Animator en_animator;
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private AudioSource audioSource;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

    }

    public override void Die()
    {
        base.Die();
        StartCoroutine(Diecountdown());
        en_animator.SetTrigger("Boom");
        SoundManager.Instance.Play(audioSource, SoundManager.Sound.EnemyDead);
        navMesh.isStopped = true;
        navMesh.speed = 0;
    }
 
    IEnumerator Diecountdown()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
