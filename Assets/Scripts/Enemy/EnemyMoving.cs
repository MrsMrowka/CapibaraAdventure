using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject leftEnd;
    [SerializeField] GameObject rightEnd;
    [SerializeField] Vector2 lookingSide;
    [SerializeField] int dmg;
    [SerializeField] float verticalPushOutStrength;
    [SerializeField] float horizontalPushOutStrength;
    [SerializeField] float disableMovementFor;

    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == leftEnd)
        {
            lookingSide = Vector2.right;
            transform.localScale = new Vector3(lookingSide.x, transform.localScale.y, transform.localScale.z);
        }
        if (collision.gameObject == rightEnd)
        {
            lookingSide = Vector2.left;
            transform.localScale = new Vector3(lookingSide.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        rb.velocity = new Vector2(lookingSide.x * moveSpeed, lookingSide.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                GlobalVariables.GetHitEnemy += 1;
                collision.gameObject.GetComponent<PlayerStats>().SubstractHealth(dmg, DeathTypes.Enemy);
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

                playerMovement.disableMovement = true;
                playerMovement.InvokeActivateMovement(disableMovementFor);

                Vector2 vector = new Vector2(playerMovement.lookingSide.x * -1 * verticalPushOutStrength, 1 * horizontalPushOutStrength);
                playerRb.velocity = vector;
            }
        }
    }
}
