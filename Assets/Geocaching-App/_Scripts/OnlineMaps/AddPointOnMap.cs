using System;
using Photon.Pun;
using UnityEngine;

[AddComponentMenu("Infinity Code/Online Maps/Examples (API Usage)/AddPointOnMap")]
public class AddPointOnMap : MonoBehaviour
{
    // Marker, which should display the location.
    private OnlineMapsMarker playerMarker;

    private OnlineMapsMarkerManager _markerManager;
    private OnlineMapsLocationService _locationService;

    public void OnButtonCreateMarker()
    {
        // Create a new marker.
        playerMarker = OnlineMapsMarkerManager.CreateItem(new Vector2(0, 0), null, "Player");
        Debug.Log("Marker Created: " + playerMarker);

        _locationService = OnlineMapsLocationService.instance;
        Debug.Log("Got location service: " + _locationService);

        if (_locationService == null)
        {
            Debug.LogError(
                "Location Service not found.\nAdd Location Service Component (Component / Infinity Code / Online Maps / Plugins / Location Service).");
            return;
        }


        // Subscribe to the change location event.
        _locationService.OnLocationChanged += OnLocationChanged;
        Debug.Log("Subscribe to the change location event");
    }

    // When the location has changed
    private void OnLocationChanged(Vector2 position)
    {
        // Change the position of the marker.
        Debug.Log("Da li si usao?");
        playerMarker.position = position;
        Debug.Log("Marker set to gps position: " + position);

        // Redraw map.
        OnlineMaps.instance.Redraw();
        Debug.Log("map redraw-ed");
    }
}