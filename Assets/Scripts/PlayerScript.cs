using System;
using System.Collections;
using System.Collections.Generic;

using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : NetworkBehaviour

{
    public int speed = 5;
    private SceneScript sceneScript;
    private string playerName = "";
    private Rigidbody rb;
    
    [SyncVar]
    public List<GameObject> skins = new List<GameObject>();
    
    [SyncVar]
    public Skin selectedSkin;

    [SyncVar]
    public Vector3 Control;

    private void Awake()
    {
        sceneScript = GameObject.FindObjectOfType<SceneScript>();
   
        //rb = GetComponent<Rigidbody>();
    }

    
    public override void OnStartAuthority()
    {
        if (isLocalPlayer)
        {
            Debug.Log(selectedSkin);
            
            setSkin();
        }
    }

    public override void OnStartLocalPlayer()
    {
       
    }

    [Command]
    public void CmdSendPlayerMessage()
    {
        if (sceneScript)
        {
            sceneScript.statusText = $"{playerName} says hello ";
        }
    }

    void Update()
    {

        if (GetComponent<NetworkIdentity>().hasAuthority)
        {
            Control = new Vector3(Input.GetAxis("Horizontal")*.2f, 0, Input.GetAxis("Vertical")*.2f);
            GetComponent<PhysicsLink>().ApplyForce(Control,ForceMode.VelocityChange);
            if (Input.GetAxis("Cancel")==1)
            {
                GetComponent<PhysicsLink>().CmdResetPose();
            }
        }

        if (!isLocalPlayer)
        {
            return;
        }
        
        // if (Input.GetButtonDown("Jump"))
        // {
        //     rb.AddForce(Vector3.back * 40f);
        // }
        
        //float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110f;
        // float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        //
         //transform.Rotate(0, moveX, 0);
        // transform.Translate(0, 0, moveZ);
        
     
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log(other.gameObject.tag);

        if (other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Damage"))
        {
            GetComponent<PlayerHealth>().TakeDamage(20);
        }
    }

    [Command]
    public void pushPlayer()
    {
        
    }

    [ClientRpc]
    public void setSkin()
    {
        foreach (var skin in skins)
        {
            if (skin.name.Equals(selectedSkin.ToString()))
            {
                skin.SetActive(true);
            }
        }
    }
}