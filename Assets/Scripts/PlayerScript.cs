using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerScript : NetworkBehaviour

{
    public int speed = 5;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }


        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110f;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Rotate(0, moveX, 0);
        transform.Translate(0, 0, moveZ);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Damage"))
        {
            GetComponent<PlayerHealth>().TakeDamage(20);
        }
    }
}