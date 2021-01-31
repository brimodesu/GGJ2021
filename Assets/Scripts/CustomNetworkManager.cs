using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;


public struct CreatePlayerMessage : NetworkMessage
{
    public Skin2 skin;
    public string name;
}

public enum Skin2
{
    Dogger,
    Tiburoncin,
    Rhino,
    Wolfang
}

public class CustomNetworkManager : NetworkManager
{
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreateCharacter);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        // you can send the message here, or wherever else you want
        CreatePlayerMessage characterMessage = new CreatePlayerMessage
        {
            name = "Player",
            skin = (Skin2) RandomEnumValue<Skin2>()
        };
       
        conn.Send(characterMessage);
    }
    
    void OnCreateCharacter(NetworkConnection conn, CreatePlayerMessage message)
    {
        // playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example
        GameObject gameobject = Instantiate(playerPrefab);

        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        PlayerScript player = gameobject.GetComponent<PlayerScript>();
        Debug.Log(message.skin);
        player.name = message.name;
        player.selectedSkin = message.skin;

        // call this to use this gameobject as the primary controller
        NetworkServer.AddPlayerForConnection(conn, gameobject);
    }
    public static T RandomEnumValue<T>()
    {
        var values = Enum.GetValues(typeof(T));
        int random = UnityEngine.Random.Range(0, values.Length);
        return (T)values.GetValue(random);
    }
}