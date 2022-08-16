using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;


public class Launcher : MonoBehaviourPunCallbacks
{
    [Tooltip(
        "The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 40; //??

    string gameVersion = "1";

    bool isConnecting;

    [SerializeField] private GameObject connectingText;
    [SerializeField] private GameObject joinButton;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    public void Connect()
    {
        joinButton.SetActive(false);
        connectingText.SetActive(true);

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }


    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }

        Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        isConnecting = false;
        Destroy(this.gameObject);
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(
            "Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = maxPlayersPerRoom});
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        PhotonNetwork.LoadLevel("Loading Nenad");
    }
}