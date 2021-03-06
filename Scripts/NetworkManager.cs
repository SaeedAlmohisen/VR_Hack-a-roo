using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        connectToServer();   
    }

    // Update is called once per frame
    public void connectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Attempting server connection...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connection successful.");
        base.OnConnectedToMaster();
        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = 10,
            IsVisible = false,
            IsOpen = true
        };
        PhotonNetwork.JoinOrCreateRoom("Room 1",roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
