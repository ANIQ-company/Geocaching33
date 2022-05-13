using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanedItemsCounter : MonoBehaviour
{
    [SerializeField] private Text collectedItemsText;

    private QRCodeScaner _qrCodeScaner;

    public int scanedItems = 0;
    public int numberOfItems = 6;

    private void Start()
    {
        collectedItemsText.text = scanedItems.ToString() + " / " + numberOfItems.ToString();
    }

    private void Update()
    {
        if (scanedItems > numberOfItems)
        {
            scanedItems = numberOfItems;
        }

        collectedItemsText.text = scanedItems.ToString() + " / " + numberOfItems.ToString();
    }
}