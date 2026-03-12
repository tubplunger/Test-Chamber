using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public int moneyValue = 5;

    public WaveManager waveManager;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log(gameObject.name + " took " + damage + " damage. Remaining: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died.");

        if (waveManager != null)
        {
            waveManager.EnemyDied();
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.AddMoney(moneyValue);
        }

        Destroy(gameObject);
    }
}
