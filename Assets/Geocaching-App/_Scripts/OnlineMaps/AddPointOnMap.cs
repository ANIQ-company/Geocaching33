using System;
using Photon.Pun;
using UnityEngine;

[AddComponentMenu("Infinity Code/Online Maps/Examples (API Usage)/AddPointOnMap")]
public class AddPointOnMap : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private GameObject userCanvas;
    [SerializeField] private GameObject adminCanvas;
    [SerializeField] private GameObject flagPrefab;
    double lng, lat;

    public PhotonView _photonView;
    
    private void Start()
    {
        OnlineMapsControlBase.instance.OnMapClick += OnMapClick;
        _photonView = PhotonView.Get(this);
    }

    public void OnMapClick()
    {
        if (adminCanvas.activeInHierarchy)
        {
            string label = CreateLocationAndLabel();

            _photonView.RPC("RpcSendMessage", RpcTarget.AllBufferedViaServer, lng, lat, label);
            Debug.Log("Photon view sent message!");
        }
    }

    string CreateLocationAndLabel()
    {
        // Get the coordinates under the cursor.
        OnlineMapsControlBase.instance.GetCoords(out lng, out lat);

        // Create a label for the marker.
        string label = "Marker " + (OnlineMapsMarkerManager.CountItems + 1);
        return label;
    }

    [PunRPC]
    private void RpcSendMessage(double lng, double lat, string label)
    {
        Debug.Log("Create marker"!);
        OnlineMapsMarkerManager.CreateItem(lng, lat, label);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //ni�ta ne �aljemo, nisam ni siguran da ne�to samim time i primamo ali neka ovo bude ovdje dok ne istestiramo.
        }
        else
        {
            // Network player, receive data
            stream.ReceiveNext();
        }

    }
}

