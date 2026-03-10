using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerAttack playerAttack;

    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        // Left click for ranged
        if (Input.GetMouseButtonDown(0))
        {
            playerAttack.RangedAttack();
        }

        // Right click for melee
        if (Input.GetMouseButtonDown(1))
        {
            playerAttack.MeleeAttack();
        }
    }
}
