using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossStats : CharacterStats
{
    [SerializeField] private Animator en_animator;
    [SerializeField] private NavMeshAgent navMesh;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject enemies;

    public delegate void HealthChangedDelegate(int currentHealth, int maxHealth);

    public event HealthChangedDelegate EventHealthChanged;

    private bool isEnraged;

    private void Update()
    {
        if (isEnraged)
        {
            damage = 20;

            enemies.gameObject.SetActive(true);
        }
    }
    
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (currentHealth <= (maxHealth / 2))
        {
            isEnraged = true;
        }

        EventHealthChanged?.Invoke(currentHealth, maxHealth);
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
