using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossManager : MonoBehaviour
{
    [SerializeField] private BossStats bossStats;
    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private bool canMove;
    [SerializeField] private bool canAtk;

    public float lookRadius = 10f;
    public GameObject e_Weapon;
    public Animator e_Attack;

    Transform target;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        target = gameObject.transform;
        agent = GetComponent<NavMeshAgent>();

        if (!canMove)
        {
            agent.stoppingDistance = lookRadius;
        }
    }

    public void activateWeapon()
    {
        e_Weapon.GetComponent<Collider>().enabled = true;
    }

    public void deactivateWeapon()
    {
        e_Weapon.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);
            }

            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }

            if (distance <= 8.0f && bossStats.currentHealth > 0 && canAtk)
            {
                e_Attack.SetTrigger("Attack");

                bossAttack.EnemyAttacking();
            }
        }

        void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}



