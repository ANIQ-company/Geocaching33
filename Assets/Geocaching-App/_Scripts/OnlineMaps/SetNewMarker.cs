using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;

public class SetNewMarker : MonoBehaviour
{
    [SerializeField] private GameObject flagPrefab;
    
    private OnlineMapsMarker playerMarker;
    public PhotonView photonView;

    private OnlineMapsInteractiveElementManager<OnlineMapsMarkerManager, OnlineMapsMarker> _onlineMapsInteractiveElements;

    private void Start()
    {
        _onlineMapsInteractiveElements =
            FindObjectOfType<OnlineMapsInteractiveElementManager<OnlineMapsMarkerManager, OnlineMapsMarker>>();
        photonView = PhotonView.Get(this);
    }


    public void RemoveMarker()
    {
        _onlineMapsInteractiveElements.Remove(playerMarker, true);
    }
    public void OnButtonAddMarker()
    {
        
        // Create a new marker.
        playerMarker = OnlineMapsMarkerManager.CreateItem(new Vector2(0, 0), null,"Marker");

        // Get instance of LocationService.
        OnlineMapsLocationService locationService = OnlineMapsLocationService.instance;

        if (locationService == null)
        {
            Debug.LogError(
                "Location Service not found.\nAdd Location Service Component (Component / Infinity Code / Online Maps / Plugins / Location Service).");
            return;
        }

        // Subscribe to the change location event.
        locationService.OnLocationChanged += OnLocationChanged;
    }

    // When the location has changed
    private void OnLocationChanged(Vector2 position)
    {
        // Change the position of the marker.
        playerMarker.position = position;

        // Redraw map.
        OnlineMaps.instance.Redraw();
    }
}