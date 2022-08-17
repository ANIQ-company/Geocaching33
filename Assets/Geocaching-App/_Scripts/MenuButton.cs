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
        MapPlane,
        removeMarkerButton;

    [SerializeField] private Sprite hamburgerMenu, xMenu;
    [SerializeField] private Image adminDropdownImage, userDropdownImage;
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
            removeMarkerButton.SetActive(false);
            adminDropdownImage.sprite = xMenu;
            adminText.text = PlayerPrefs.GetString("admin");
        }
        else if (adminPanel.activeSelf)
        {
            adminDropdownImage.sprite = hamburgerMenu;
            removeMarkerButton.SetActive(true);
            adminPanel.SetActive(false);
        }
    }

    public void UserDropdownMenuButton()
    {
        if (!userPanel.activeSelf)
        {
            userPanel.SetActive(true);
            userDropdownImage.sprite = xMenu;
            userText.text = PlayerPrefs.GetString("username");
        }
        else if (userPanel.activeSelf)
        {
            userDropdownImage.sprite = hamburgerMenu;
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