using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int playerMaxHealth;
    [SerializeField] List<Image> playerHearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] float invincibilityFrames;

    private bool canTakeDmg;
    Death deathClass;
    int playerCurrentHealth;
    PlayerMovement playerMovement;

    private void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        deathClass = GameObject.FindObjectOfType<Death>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        canTakeDmg = true;
    }

    private void CheckHealth(DeathTypes deathType)
    {
        if (playerCurrentHealth <= 0)
        {
            deathClass.PlayerDeath(deathType);
        }
    }

    public void AddHealth(int amount)
    {
        if (playerCurrentHealth < playerMaxHealth) {
            GlobalVariables.RecoveredHP += amount;
            playerHearts[playerCurrentHealth].sprite = fullHeart;
            playerCurrentHealth += amount;
        }
        
        Debug.Log("Current HP: " + playerCurrentHealth);
    }

    public void SubstractHealth(int amount, DeathTypes deathType)
    {
        if (canTakeDmg)
        {
            if (amount <= 0)
            {
                AddHealth(amount * -1);
            } else
            {
                GlobalVariables.LostHP += amount;
                playerCurrentHealth -= amount;
                playerHearts[playerCurrentHealth].sprite = emptyHeart;
                Debug.Log("Current HP: " + playerCurrentHealth);
                CheckHealth(deathType);
                playerMovement.TakesDmg();
            }
            canTakeDmg = false;
            Invoke(nameof(InvincibilityFrames), invincibilityFrames);
        }
    }

    void InvincibilityFrames()
    {
        canTakeDmg = true;
    }
}
