using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

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
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRoom("Classroom");
            Debug.Log("Room Classroom joined");
        }
        else
        {
            RoomOptions roomOptions = new RoomOptions()
            {
                MaxPlayers = 10,
                IsVisible = true,
                IsOpen = true
            };
            PhotonNetwork.JoinOrCreateRoom("Classroom", roomOptions, TypedLobby.Default);
            Debug.Log("Creating new room Classroom");
        }
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
