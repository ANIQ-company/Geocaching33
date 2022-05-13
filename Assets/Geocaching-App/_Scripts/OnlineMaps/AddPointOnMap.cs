using System;
using Photon.Pun;
using UnityEngine;

[AddComponentMenu("Infinity Code/Online Maps/Examples (API Usage)/AddPointOnMap")]
public class AddPointOnMap : MonoBehaviour
{
    [SerializeField] private GameObject userCanvas;
    [SerializeField] private GameObject adminCanvas;

    // Marker, which should display the location.
    private OnlineMapsMarker playerMarker;

    private OnlineMapsMarkerManager _markerManager;
    private OnlineMapsLocationService _locationService;

    private void Start()
    {
        // Subscribe to the click event.
        OnlineMapsControlBase.instance.OnMapClick += OnMapClick;
    }

    public void OnMapClick()
    {
        if (adminCanvas.activeInHierarchy)
        {
            // Get the coordinates under the cursor.
            double lng, lat;
            OnlineMapsControlBase.instance.GetCoords(out lng, out lat);

            // Create a label for the marker.
            string label = "Marker " + (OnlineMapsMarkerManager.CountItems + 1);

            // Create a new marker.
            OnlineMapsMarkerManager.CreateItem(lng, lat, label);
        }
    }
}