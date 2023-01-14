using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] LobbyManager lobbySystem;
    [SerializeField] byte playerRoomMax = 2;

    void StartGame()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        string roomName = "Room" + Random.Range(1000, 10000);

        // Room properties
        RoomOptions roomOptions = new RoomOptions()
        {
            IsOpen = true,
            IsVisible = true,
            MaxPlayers = playerRoomMax
        };

        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
        Debug.Log(roomName);
    }

    public override void OnConnected() {
        Debug.Log("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        lobbySystem.PanelLobbyActive(true);
        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");
        if(PhotonNetwork.CurrentRoom.PlayerCount == playerRoomMax)
        {
            foreach(var player in PhotonNetwork.PlayerList)
            {
                if(player.IsMasterClient)
                {
                    StartGame();
                    Hashtable props = new Hashtable
                    {
                        {CountdownTimer.CountdownStartTime, (float) PhotonNetwork.Time}
                    };
                    PhotonNetwork.CurrentRoom.SetCustomProperties(props);
                }
            }
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if(propertiesThatChanged.ContainsKey(CountdownTimer.CountdownStartTime))
            lobbySystem.lobbyStartTime.gameObject.SetActive(true);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnConnectedToMaster: " + cause.ToString());
        lobbySystem.PanelLobbyActive(false);
    }

    public void CancelMatch()
    {
        PhotonNetwork.Disconnect();
        lobbySystem.PanelLobbyActive(false);
    }

    public void SearchMatch()
    {
        PhotonNetwork.NickName = lobbySystem.playerNameInputField.text;
        lobbySystem.PanelLobbyActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }
}