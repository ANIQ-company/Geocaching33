using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

[AddComponentMenu("Infinity Code/Online Maps/Examples (API Usage)/AddPointOnMap")]
public class AddPointOnMap : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private GameObject userCanvas;
    [SerializeField] private GameObject adminCanvas;
    [SerializeField] private GameObject flagPrefab;
    [SerializeField] private TMP_Text addedMarkerText;
    double lng, lat;

    private List<int> index;
    private int number;
    private bool isCoroutineActive;
    private IEnumerator activeCoroutine;
    
    public PhotonView _photonView;

    //private int countItems = OnlineMapsMarkerManager.CountItems + 1;
    private OnlineMapsInteractiveElementManager<OnlineMapsMarkerManager, OnlineMapsMarker>
        _onlineMapsInteractiveElements;

    private void Start()
    {
        _onlineMapsInteractiveElements =
            FindObjectOfType<OnlineMapsInteractiveElementManager<OnlineMapsMarkerManager, OnlineMapsMarker>>();
        OnlineMapsControlBase.instance.OnMapClick += OnMapClick;
        _photonView = PhotonView.Get(this);
        number = 1;
    }

    public void RemoveMarker()
    {
        if (number == 1)
        {
            return;
        }

        _onlineMapsInteractiveElements.Remove(OnlineMapsMarkerManager.RemoveItemAt(number), true);
        number--;
    }

    public void OnMapClick()
    {
        if (adminCanvas.activeInHierarchy)
        {
            string label = CreateLocationAndLabel();

            _photonView.RPC("RpcSendMessage", RpcTarget.AllBufferedViaServer, lng, lat, label);
            Debug.Log("Photon view sent message!");

            number++;

            StartCoroutine(nameof(AddPointText));
            isCoroutineActive = true;
        }
    }

    private IEnumerator AddPointText()
    {
        if (isCoroutineActive)
        {
            yield return new WaitForSeconds(5);
            addedMarkerText.text = "";
            isCoroutineActive = false;
        }
        else
        {
            StopCoroutine(nameof(AddPointText));
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
        addedMarkerText.text = "You have added marker on: " + "\nLongitude: " + lng + "\nLangitude: " + lat;
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