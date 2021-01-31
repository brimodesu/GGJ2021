using System;
using Mirror;
using UnityEngine;


    public class Cube : NetworkBehaviour
    {
        public Rigidbody rb;

        [SyncVar]//all the essental varibles of a rigidbody
        public Vector3 Velocity;
        [SyncVar]
        public Quaternion Rotation;
        [SyncVar]
        public Vector3 Position;
        [SyncVar]
        public Vector3 AngularVelocity;
        
        private void Update()
        {
          
                Position = rb.position;
                Rotation = rb.rotation;
                Velocity = rb.velocity;
                AngularVelocity = rb.angularVelocity;
                rb.position = Position;
                rb.rotation = Rotation;
                rb.velocity = Velocity;
                rb.angularVelocity = AngularVelocity;
         
             
        }
    }
