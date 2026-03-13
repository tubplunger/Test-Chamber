using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public static HealthSystem instance;

    public int maxHealth = 10;
    private int currentHealth;

    public int moneyValue = 5;

    public WaveManager waveManager;

    public Slider healthBar;

    SpriteRenderer sprite;

    Color originalColor;

    public float flashDuration = 0.1f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;

        sprite = GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
            originalColor = sprite.color;
        }
    }

    IEnumerator FlashRed()
    {
        if (sprite == null) yield break;

        sprite.color = Color.red;

        yield return new WaitForSeconds(flashDuration);

        sprite.color = originalColor;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        StartCoroutine(FlashRed());

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        Debug.Log(gameObject.name + " took " + damage + " damage. Remaining: " + currentHealth);

        if (currentHealth <= 0)
        {
            if (CompareTag("Player"))
            {
                GameManager.instance.GameOver();
            }
            else
            {
                gameObject.SetActive(false);
            }

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

        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void UpgradeMaxHealth()
    {
        maxHealth += 10;
    }

    public void UpgradeMoneyDrop()
    {
        moneyValue += 2;
    }
}
