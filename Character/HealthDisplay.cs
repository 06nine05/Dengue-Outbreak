using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private PlayerStats health = null;
    [SerializeField] private Image healthBarImage = null;

    private void OnEnable()
    {
        health.EventHealthChanged += HandleHealthChanged;
    }

    private void OnDisable()
    {
        health.EventHealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(int currentHealth, int maxHealth)
    {
        healthBarImage.fillAmount = (float)currentHealth / maxHealth;
    }
}
