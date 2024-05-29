using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCollection : MonoBehaviour
{

    PlayerStats playerStats;

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Coin"))
            {
                GlobalVariables.CoinsCollected += collision.GetComponent<Coin>().value;
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.CompareTag("Key"))
            {
                GlobalVariables.KeysCollected += 1;
                collision.GetComponent<Key>().OpenDoor();
                collision.gameObject.SetActive(false);
            }
            if (collision.gameObject.CompareTag("HealthPack"))
            {
                playerStats.AddHealth(collision.GetComponent<HealthPack>().amountToHeal);
                collision.gameObject.SetActive(false);
            }
        }
    }
}
