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
        removeMarkerButton,
        addPointOnMapText;

    [SerializeField] private Sprite hamburgerMenu, xMenu;
    [SerializeField] private Image adminDropdownImage, userDropdownImage;

    [SerializeField] private OnlineMapsMarkerManager _markerManager;
    //[SerializeField] private AddPointOnMap _addPointOnMapText;
    
    private UserList _userList;

    private void Start()
    {
        _userList = FindObjectOfType<UserList>();
        _markerManager = GetComponent<OnlineMapsMarkerManager>();
        
    }

    public void AdminDropdownMenuButton()
    {
        if (!adminPanel.activeSelf)
        {
            adminPanel.SetActive(true);
            removeMarkerButton.SetActive(false);
            adminDropdownImage.sprite = xMenu;
            addPointOnMapText.SetActive(false);
            adminText.text = PlayerPrefs.GetString("admin");
        }
        else if (adminPanel.activeSelf)
        {
            adminDropdownImage.sprite = hamburgerMenu;
            removeMarkerButton.SetActive(true);
            addPointOnMapText.SetActive(true);
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