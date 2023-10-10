using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class JumperBall : Ball
{
    void Update()
    {
        if (!isAttacking)
        {
            if (Input.GetKey(KeyCode.A))
            {
                MoveLeft();
            }
            if (Input.GetKey(KeyCode.D))
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
        }
    }

    public override void Attack()
    {
        isAttacking = true;
        float temp_mass = rb.mass;
        rb.mass = attackMass;
        rb.velocity = new Vector3(0, attackForce, 0);
        rb.mass = temp_mass;
    }

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, specialForce, 0);
    }
}
