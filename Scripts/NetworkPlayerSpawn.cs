using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawn : MonoBehaviourPunCallbacks
{

    private GameObject spawnedPlayerPrefab;
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("NetworkPlayer", transform.position, transform.rotation);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Player has left the room");
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
