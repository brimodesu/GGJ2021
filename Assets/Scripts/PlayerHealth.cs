using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : NetworkBehaviour
{
    public const int maxHealth = 100;

    public Text txtLife;
    public int currentHealth = maxHealth;

    public override void OnStartAuthority()
    {
        if (isLocalPlayer)
        {
            //txtLife.text = currentHealth.ToString("0000");            
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            currentHealth -= amount;
        }

        if (isLocalPlayer)
        {
            if (currentHealth <= 0)
            {
                currentHealth = maxHealth;
                Debug.Log("DEAD");

                RpcRespawn();
            }
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = Vector3.zero;
        }
    }
}