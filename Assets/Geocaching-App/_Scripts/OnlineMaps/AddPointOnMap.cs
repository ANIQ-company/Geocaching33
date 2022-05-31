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
            //ništa ne šaljemo, nisam ni siguran da nešto samim time i primamo ali neka ovo bude ovdje dok ne istestiramo.
        }
        else
        {
            // Network player, receive data
            stream.ReceiveNext();
        }

    }
}


#region old
    //using System;
    //using Photon.Pun;
    //using UnityEngine;

    //[AddComponentMenu("Infinity Code/Online Maps/Examples (API Usage)/AddPointOnMap")]
    //public class AddPointOnMap : MonoBehaviour, IPunObservable
    //{
    //    [SerializeField] private GameObject userCanvas;
    //    [SerializeField] private GameObject adminCanvas;

    //    public PhotonView _photonView;

    //    // Marker, which should display the location.
    //    private OnlineMapsMarker playerMarker;

    //    private OnlineMapsMarkerManager _markerManager;
    //    private OnlineMapsLocationService _locationService;

    //    private void Start()
    //    {
    //        // Subscribe to the click event.
    //        OnlineMapsControlBase.instance.OnMapClick += OnMapClick;
    //    }

    //    public void OnMapClick()
    //    {
    //        if (adminCanvas.activeInHierarchy)
    //        {
    //            // Get the coordinates under the cursor.
    //            double lng, lat;
    //            OnlineMapsControlBase.instance.GetCoords(out lng, out lat);

    //            // Create a label for the marker.
    //            string label = "Marker " + (OnlineMapsMarkerManager.CountItems + 1);

    //            // Create a new marker.
    //            OnlineMapsMarkerManager.CreateItem(lng, lat, label);
    //            _photonView.RPC(nameof(SendMessage), RpcTarget.AllBuffered, lat, lng);
    //            Debug.Log("Photon view sent message!");
    //        }
    //    }

    //    [PunRPC]
    //    private void SendMessage(double lng, double lat, string label)
    //    {
    //        Debug.Log("Create marker"!);
    //        OnlineMapsMarkerManager.CreateItem(lng, lat, label);
    //    }


    //    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //    {
    //        Debug.Log("Photon Serialized view");
    //        double lng, lat;
    //        OnlineMapsControlBase.instance.GetCoords(out lng, out lat);
    //        Debug.Log("Longitude: " + lng + "\n Latitude: " + lat);

    //        string label = "Marker " + (OnlineMapsMarkerManager.CountItems + 1);
    //        stream.SendNext(OnlineMapsMarkerManager.CreateItem(lng, lat, label));
    //        Debug.Log("Send to server to create marker");

    //    }
    //}
#endregion