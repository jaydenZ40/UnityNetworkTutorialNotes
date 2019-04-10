using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerConnection : NetworkBehaviour
{
    public GameObject PlayerUnitPrefab;
    [SyncVar(hook = "OnFunctionName")]
    public string PlayerName = "Anonymous"; // WARNING: If you use a SyncVar, then our local value does NOT get automatically updata
    void OnFunctionName(string n) { PlayerName = n; }

    void Start()
    {
        /* Another way to instantiate playerObject:
        if (!isLocalPlayer)
            return;

        Debug.Log("PlayerConnection::Start -- Spawning my own personal unit...");
        Instantiate(PlayerUnitPrefab);
        // It will only create a local object. To create one for all, use NetworkServer.Spawn()
        */

        Debug.Log("PlayerConnection::Start -- Spawning my own personal unit...");
        CmdSpawnMyUnit();
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            CmdSpawnMyUnit();
        }

        if (Input.GetKeyDown(KeyCode.Q))    //  can also write code for transform instead of using Network Transform component for PlayerUnit object
        {
            string n = "newName";
            CmdChangePlayerName(n);
        }
        
    }

    GameObject myPlayerUnit;

    // Command are special functions that ONLY get executed on the server.
    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject go = Instantiate(PlayerUnitPrefab);
        myPlayerUnit = go;

        // go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);  //or use the next line code for Option 2
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    /*
    [Command]
    void CmdMoveUnitUp()    // Option 1: Sending to server wait for response.
    {
        if (myPlayerUnit == null)
        {
            return;
        }

        myPlayerUnit.transform.Translate(Vector3.up);
    }
    */

  
    [Command]
    void CmdChangePlayerName(string n)
    {
        PlayerName = n;
        //RpcChangePlayerName(PlayerName);  // use [SyncVar] to do the same thing
    }


    // RPC are special functions that ONLY get executed on clients.

    //[ClientRpc]       // use [SyncVar] to do the same thing
    //void RpcChangePlayerName(string n)
    //{
    //    PlayerName = n;
    //}
}
