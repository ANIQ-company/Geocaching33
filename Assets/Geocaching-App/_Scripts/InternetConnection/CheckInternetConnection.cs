using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInternetConnection : MonoBehaviour
{
    [SerializeField] private Text offlineText;
    [SerializeField] private GameObject offlineCanvas;

    private void Start()
    {
        InvokeRepeating(nameof(CheckConnection), 1, 10);
    }

    public virtual void CheckConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            offlineCanvas.SetActive(true);
            Debug.Log("Error. Check internet connection!");
            offlineText.text = "Error. Check internet connection!";
        }

        else
        {
            offlineCanvas.SetActive(false);
        }
    }
}