using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{ 
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public bool canTakeDamage = true;
    public bool isDie = false;

    public int damage = 50 ;
    public int armor = 0;

    void Awake ()
    {
        canTakeDamage = true;
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public virtual void TakeDamage(int damage)
    {
        if (canTakeDamage)
        {
            damage -= armor;
            damage = Mathf.Clamp(damage, 0, maxHealth);
            canTakeDamage = false;
            currentHealth -= damage;

            StartCoroutine(TakeDamageCooldown());

            if (currentHealth <= 0)
            {
                canTakeDamage = false;

                Die();
            }
        }
    }

    public virtual void Heal(int heal)
    {
        heal = Mathf.Clamp(heal, 0, maxHealth);
        currentHealth += heal;
    }

    protected IEnumerator TakeDamageCooldown()
    {
        yield return new WaitForSeconds(1.2f);
        canTakeDamage = true;
    }

    public virtual void Die()
    {
        isDie = true;
    }
}