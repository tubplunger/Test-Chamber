using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public void MeleeAttack()
    {
        Debug.Log("Melee attack triggered!");
    }

    public void RangedAttack()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        Debug.Log("Ranged attack triggered! Direction: " + direction);
    }
}
