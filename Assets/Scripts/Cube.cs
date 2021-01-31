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
         
                if (GetComponent<NetworkIdentity>().isClient)//if we are a client update our rigidbody with the servers rigidbody info
                {
                    rb.position = Position+Velocity*(float)NetworkTime.rtt;//account for the lag and update our varibles
                    rb.rotation = Rotation*Quaternion.Euler(AngularVelocity * (float)NetworkTime.rtt);
                
                    if (rb.velocity.magnitude > Speed)
                    {
                        rb.velocity =  Vector3.ClampMagnitude(rb.velocity, Speed);
                 
                    }
                    else
                    {
                        rb.velocity = Velocity;
                   
                    }

                    rb.angularVelocity = AngularVelocity;
                }
        }
    }
