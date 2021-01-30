using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;


    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
                Debug.Log("DEAD");
        }
    }
}
