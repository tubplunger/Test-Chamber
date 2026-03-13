using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack instance;

    public int meleeDamage = 3;
    public int rangedDamage = 2;

    public float meleeRange = 1.5f;
    public float rangedRange = 10f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    public LayerMask enemyLayer;

    void Awake()
    {
        instance = this;
    }

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

        GameObject projectile = ProjectilePooling.instance.GetProjectile();
        projectile.transform.position = firePoint.position;

        Projectile proj = projectile.GetComponent<Projectile>();
        proj.Initialize(direction, rangedDamage);
    }

    public void UpgradeMeleeDamage()
    {
        meleeDamage += 2;
    }

    public void UpgradeRangedDamage()
    {
        rangedDamage += 2;
    }

    public void UpgradeMeleeRange()
    {
        meleeRange += 0.2f;
    }

    public void UpgradeRangedRange()
    {
        rangedRange += 1f;
    }
}
