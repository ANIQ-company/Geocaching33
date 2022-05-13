using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkController : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are connected to the " + PhotonNetwork.CloudRegion + " server!");
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("MainScene");
    }
}