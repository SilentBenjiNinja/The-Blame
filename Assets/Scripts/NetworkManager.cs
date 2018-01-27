using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {
    public string roomName = "Room";
    public string playerPrefabName = "prefabPlayer";
    public Transform spawnPoint;

    void Start()
    {
        
    }

    void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(
            playerPrefabName,
            spawnPoint.position,
            spawnPoint.rotation,
            0);
    }
}
