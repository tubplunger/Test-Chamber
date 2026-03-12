using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public int attackDamage = 2;
    public float attackCooldown = 1.5f;

    public float separationRadius = 1.2f;
    public float separationForce = 2f;

    private Transform player;
    private float attackTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            AttackPlayer();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        Vector2 separation = GetSeparationVector();

        Vector2 moveDirection = directionToPlayer + separation;

        transform.position += (Vector3)(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    Vector2 GetSeparationVector()
    {
        Collider2D[] neighbors = Physics2D.OverlapCircleAll(
        transform.position,
        separationRadius
    );

        Vector2 separation = Vector2.zero;

        foreach (Collider2D col in neighbors)
        {
            if (col.gameObject == gameObject)
                continue;

            if (col.GetComponent<EnemyController>())
            {
                Vector2 diff = (Vector2)(transform.position - col.transform.position);

                separation += diff.normalized / diff.magnitude;
            }
        }

        return separation * separationForce;
    }

    void AttackPlayer()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCooldown)
        {
            attackTimer = 0f;

            HealthSystem playerHealth = player.GetComponent<HealthSystem>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Enemy attacked player!");
            }
        }
    }
}