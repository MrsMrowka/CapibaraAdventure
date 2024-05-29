using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] float bulletSpeed;
    [SerializeField] float verticalPushOutStrength;
    [SerializeField] float horizontalPushOutStrength;
    [SerializeField] float disableMovementFor;

    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    [HideInInspector] public float direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        rb.velocity = new Vector2(direction * bulletSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                GlobalVariables.GetHitBullet += 1;
                collision.gameObject.GetComponent<PlayerStats>().SubstractHealth(dmg, DeathTypes.Enemy);
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

                playerMovement.disableMovement = true;
                playerMovement.InvokeActivateMovement(disableMovementFor);

                Vector2 vector = new Vector2(playerMovement.lookingSide.x * -1 * verticalPushOutStrength, 1 * horizontalPushOutStrength);
                playerRb.velocity = vector;
            }
            Destroy(gameObject);
        }
    }
}
