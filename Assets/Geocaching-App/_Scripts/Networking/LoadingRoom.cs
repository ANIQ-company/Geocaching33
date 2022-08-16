using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingRoom : MonoBehaviour
{
    [SerializeField] private TextMeshPro connectingText;
    [SerializeField] private Button joinButton;



    public void OnButtonJoin()
    {
        connectingText.text = "Connecting...";
        joinButton.GetComponentInChildren<TextMeshPro>().text = connectingText.text;
    }
}
