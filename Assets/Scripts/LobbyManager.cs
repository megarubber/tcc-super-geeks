using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] GameObject panelLogin;
    [SerializeField] GameObject panelLobby;

    public Text lobbyStartTime;
    public InputField playerNameInputField;
    public string playerName;
    public Text connectionStatusText;

    void Start()
    {
        PanelLobbyActive(false);
    }

    public void PanelLobbyActive(bool state)
    {
        panelLobby.SetActive(state);
        panelLogin.SetActive(!state);
    }
}