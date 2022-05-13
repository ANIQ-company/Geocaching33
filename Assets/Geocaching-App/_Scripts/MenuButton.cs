using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] public Text
        userText,
        adminText;

    [Space]
    [SerializeField] private GameObject
        userPanel,
        adminPanel,
        scanCanvas,
        MapPlane;


    private UserList _userList;

    private void Start()
    {
        _userList = FindObjectOfType<UserList>();
    }

    public void AdminDropdownMenuButton()
    {
        if (!adminPanel.activeSelf)
        {
            adminPanel.SetActive(true);
            adminText.text = PlayerPrefs.GetString("admin");
        }
        else if (adminPanel.activeSelf)
        {
            adminPanel.SetActive(false);
        }
    }

    public void UserDropdownMenuButton()
    {
        if (!userPanel.activeSelf)
        {
            userPanel.SetActive(true);
            userText.text = PlayerPrefs.GetString("username");
        }
        else if (userPanel.activeSelf)
        {
            userPanel.SetActive(false);
        }
    }

    public void UserLogoutButton()
    {
        _userList.userCanvas.SetActive(false);
        _userList.loginCanvas.SetActive(true);
    }

    public void AdminLogoutButton()
    {
        _userList.adminCanvas.SetActive(false);
        _userList.loginCanvas.SetActive(true);
    }

    public void ScanButton()
    {
        _userList.userCanvas.SetActive(false);
        MapPlane.SetActive(false);
        scanCanvas.SetActive(true);
        
    }
}