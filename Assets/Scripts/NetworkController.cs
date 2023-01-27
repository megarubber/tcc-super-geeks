using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    [SerializeField] LobbyManager lobbySystem;
    [SerializeField] byte playerRoomMax = 2;
    private IEnumerator ss;

    void StartGame()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        ss = JoinGame();
        StartCoroutine(ss);
    }

    IEnumerator JoinGame() {
        yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount == playerRoomMax);
        StartGame();
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
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnConnectedToMaster: " + cause.ToString());
        lobbySystem.PanelLobbyActive(false);
    }

    public void CancelMatch()
    {
        StopCoroutine(ss);
        PhotonNetwork.Disconnect();
        lobbySystem.PanelLobbyActive(false);
    }

    public void SearchMatch()
    {
        PhotonNetwork.NickName = lobbySystem.playerNameInputField.text;
        Server.username = lobbySystem.playerNameInputField.text;
        lobbySystem.PanelLobbyActive(true);
        PhotonNetwork.ConnectUsingSettings();
    }
}