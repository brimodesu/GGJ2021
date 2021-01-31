using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public enum Skin
    {
        Dogger,
        Tiburoncin,
        Rhino,
        Wolfang
    }


    public class PlayerV2 : NetworkBehaviour
    {
        [Header("Components")]
        public NavMeshAgent agent;
        

        [Header("Movement")]
        public float rotationSpeed = 100;

        [Header("Firing")]
        public KeyCode shootKey = KeyCode.Space;
        public GameObject projectilePrefab;
        public Transform projectileMount;

        [Header("Skin")]
        public List<GameObject> skins = new List<GameObject>();
        
        [SyncVar(hook = nameof(OnSkinChanged))] 
        public string selectedSkin;


        public Rigidbody rb;
        
        public override void OnStartLocalPlayer()
        {
            string[] skins = { "Dogger",
                "Tiburoncin",
                "Rhino",
                "Wolfang" };
            
            CmdSetupPlayer(skins[Random.Range (0, skins.Length)]);
        }

        void Update()
        {
            // movement for local player
            if (!isLocalPlayer) return;

            // rotate
            float horizontal = Input.GetAxis("Horizontal");
            transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

            // move
            float vertical = Input.GetAxis("Vertical");
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            rb.velocity = forward * (vertical * 5);
            

            // shoot
            if (Input.GetKeyDown(shootKey))
            {
                //CmdFire();
            }
        }

        void OnSkinChanged(string _Old, string _New) {
            foreach (var skin in skins)
            {
                if (skin.name.Equals(selectedSkin))
                {
                    skin.SetActive(true);
                }
            }
        }

        // this is called on the server
        [Command]
        void CmdFire()
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileMount.position, transform.rotation);
            NetworkServer.Spawn(projectile);
            RpcOnFire();
        }

        // this is called on the tank that fired for all observers
        [ClientRpc]
        void RpcOnFire()
        {
           // animator.SetTrigger("Shoot");
        }

        [Command]
        public void CmdSetupPlayer(string skin) {
            selectedSkin = skin;
        }

        public static T RandomEnumValue<T>()
        {
            var values = Enum.GetValues(typeof(T));
            int random = UnityEngine.Random.Range(0, values.Length-1);
            return (T)values.GetValue(random);
        }
    }

