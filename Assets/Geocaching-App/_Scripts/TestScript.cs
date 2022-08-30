using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private GameObject 
        adminPanel,
        userPanel,
        matPlane;


    public void TestButton()
    {
        if (adminPanel.activeInHierarchy)
        {
            adminPanel.SetActive(false);
            matPlane.SetActive(false);
            userPanel.SetActive(true);
        }
        
    }
}