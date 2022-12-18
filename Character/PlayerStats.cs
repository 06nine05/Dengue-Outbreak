using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStats : CharacterStats
{
    [SerializeField] private PlayerController controller;

    public delegate void HealthChangedDelegate(int currentHealth, int maxHealth);

    public event HealthChangedDelegate EventHealthChanged;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        base.Die();
        StartCoroutine(Diecountdown());
        controller.Die();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        EventHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public override void Heal(int heal)
    {
        base.Heal(heal);

        EventHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    IEnumerator Diecountdown()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
