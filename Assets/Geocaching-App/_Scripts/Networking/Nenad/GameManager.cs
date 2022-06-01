using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public int sceneToLoad;

    PhotonView view;

    private void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            LoadMainScene();
        }
        else
        {
            view = PhotonView.Get(this);
        }
    }
    public void RequestUsernameUpdate(string name)
    {
        PhotonNetwork.LocalPlayer.NickName = name;
        print(name);
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(sceneToLoad);
    }   

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }

    void LoadMainScene()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);             //player count je tu prakticki bezveze ak netreba il smeta javi pa cu ga maknut
        PhotonNetwork.LoadLevel("MainScene Nenad");                                                               //pazi ovdje je na ime scene ako ces renamat!!
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // nicknameove ces vjerovatno povuc iz username ako je tak javi da to povezem
    }


    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName);


        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);
        }
    }
}
