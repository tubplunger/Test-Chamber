using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int meleeDamage = 3;
    public int rangedDamage = 2;

    public float meleeRange = 1.5f;
    public float rangedRange = 10f;

    public LayerMask enemyLayer;

    public void MeleeAttack()
    {
        Debug.Log("Melee attack!");

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, meleeRange, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            HealthSystem health = enemy.GetComponent<HealthSystem>();

            if (health != null)
            {
                health.TakeDamage(meleeDamage);
            }
        }
    }

    public void RangedAttack()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        Vector2 direction = (mouseWorld - transform.position).normalized;

        Debug.Log("Ranged attack fired!");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rangedRange, enemyLayer);

        if (hit.collider != null)
        {
            HealthSystem health = hit.collider.GetComponent<HealthSystem>();

            if (health != null)
            {
                health.TakeDamage(rangedDamage);
            }
        }
    }
}
