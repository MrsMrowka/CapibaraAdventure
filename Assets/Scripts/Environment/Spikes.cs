using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] float verticalPushOutStrength;
    [SerializeField] float horizontalPushOutStrength;
    [SerializeField] float disableMovementFor;
    [SerializeField] int dmg;

    private PlayerMovement playerMovement;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Player")
        {
            GlobalVariables.HitSpikes += 1;
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();

            playerStats.SubstractHealth(dmg, DeathTypes.Obstacle);

            playerMovement.disableMovement = true;
            playerMovement.InvokeActivateMovement(disableMovementFor);

            Vector2 vector = new Vector2(playerMovement.lookingSide.x * -1 * verticalPushOutStrength, 1 * horizontalPushOutStrength);
            rb.velocity = vector;
        }
    }
}
