using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class BoosterBall : Ball
{
    private bool isBoosting;
    private float boostDuration = 0.3f;
    private int boostDirection;

    void Update()
    {
        // allow directional changes even if attacking so player can save themselves by boosting
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            boostDirection = -1;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            boostDirection = 1;

        if (!isAttacking && !isBoosting)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Attack();
            }
        }

        if (Input.GetKeyDown(KeyCode.L) && !isBoosting)
            StartCoroutine(BoostRoutine());
    }

    public override void Attack()
    {
        isAttacking = true;
        float temp_mass = rb.mass;
        rb.mass = attackMass;
        rb.velocity = new Vector3(0, attackForce, 0);
        rb.mass = temp_mass;
    }

    public void Boost()
    {
        if (Mathf.Abs(boostDirection) == 1)
        {
            rb.velocity = new Vector3(specialForce * boostDirection, rb.velocity.y, 0);
        }
    }

    private IEnumerator BoostRoutine()
    {
        isBoosting = true;
        Boost();
        yield return new WaitForSeconds(boostDuration);
        isBoosting = false;
    }
}
