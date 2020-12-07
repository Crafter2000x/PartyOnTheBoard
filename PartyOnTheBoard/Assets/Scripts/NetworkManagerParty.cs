using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NetworkManagerParty : NetworkManager
{
    [SerializeField] private int minPlayers = 2;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public static event Action OnPlayerObjectCreated;
    public static event Action OnPlayerObjectRemoved;

    public List<GameObject> PartyPlayers;

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        PartyPlayers.Add(player);
        NetworkServer.AddPlayerForConnection(conn, player);
        OnPlayerObjectCreated?.Invoke();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.gameObject;
            PartyPlayers.Remove(player);
        }

        base.OnServerDisconnect(conn);
        OnPlayerObjectRemoved.Invoke();
    }

    public override void OnStopServer()
    {
        PartyPlayers.Clear();
    }
}
