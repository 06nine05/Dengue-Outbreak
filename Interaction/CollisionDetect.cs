using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    public Attack Ak;
    //public GameObject HitParticle;

    [SerializeField]
    private CharacterStats characterStats;

    void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Fake") && Ak.IsAttacking)
        {
            other.gameObject.GetComponent<EnemyStats>().TakeDamage(characterStats.damage);
        }

        else if (other.gameObject.tag == "Boss" && Ak.IsAttacking)
        {
            other.gameObject.GetComponent<BossStats>().TakeDamage(characterStats.damage);
        }

        else if (other.gameObject.tag == "Vase" && Ak.IsAttacking)
        {
            Debug.Log("Working");
            Destroy(other.gameObject);
        }
    }
}
