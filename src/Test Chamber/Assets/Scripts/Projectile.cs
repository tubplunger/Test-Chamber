using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 3f;
    public int damage;

    private Vector2 direction;

    public void Initialize(Vector2 dir, int damageValue)
    {
        direction = dir.normalized;
        damage = damageValue;

        Invoke(nameof(Deactivate), lifetime);
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        HealthSystem health = other.GetComponent<HealthSystem>();

        if (health != null)
        {
            health.TakeDamage(damage);
            Deactivate();
        }
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        CancelInvoke();
    }
}
