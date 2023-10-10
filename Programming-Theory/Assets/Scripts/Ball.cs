using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] protected float attackForce = 10.0f;
    [SerializeField] protected float specialForce = 10.0f;
    [SerializeField] protected float attackMass = 10.0f;

    protected Rigidbody rb;

    private string playerName;
    private int score = 0;
    private Color ballColor;
    protected float radius = 0.5f;
    protected bool isAttacking = false;
    private GameManager gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public string PlayerName
    {
        get { return playerName; }
        set
        {
            if (value.Length <= 10)
            {
                playerName = value;
            }
            else
            {
                Debug.Log("Names must be less than 10 characters");
            }
        }
    }

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
        }
    }

    public Color BallColor
    {
        get { return ballColor; }
        set { ballColor = value; }
    }

    public void MoveLeft()
    {
        rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0);
    }
    public void MoveRight()
    {
        rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0);
    }
    public abstract void Attack();

    void OnCollisionEnter()
    {
        isAttacking = false;
    }

    void OnTriggerEnter()
    {
        gameManager.BallFellOff(this);
    }
}
