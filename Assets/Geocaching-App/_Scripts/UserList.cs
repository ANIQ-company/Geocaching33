using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserList : MonoBehaviour
{
    [SerializeField] public GameObject
        adminCanvas,
        loginCanvas,
        userCanvas;

    [SerializeField] private Text errorText;

    [Space] //public InputField userNameField;
    public TMP_InputField userNameField;
   //public InputField passwordField;
    public TMP_InputField passwordField;
    public Button loginButton;

    private PlayerPrefs usernameSave;

    private readonly Dictionary<string, string> adminLoginDetails = new Dictionary<string, string>
    {
        {"admin0", "admin0"},
        {"admin1", "admin1"}
    };

    private readonly Dictionary<string, string> loginDetails = new Dictionary<string, string>
    {
        {"user0", "user0"},
        {"user1", "user1"},
        {"user2", "user2"},
        {"user3", "user3"},
        {"user4", "user4"},
        {"user5", "user5"},
        {"user6", "user6"},
        {"user7", "user7"},
        {"user8", "user8"},
        {"user9", "user9"},
        {"user10", "user10"},
        {"user11", "user11"},
        {"user12", "user12"},
        {"user13", "user13"},
        {"user14", "user14"},
        {"user15", "user15"},
        {"user16", "user16"},
        {"user17", "user17"},
        {"user18", "user18"},
        {"user19", "user19"},
        {"user20", "user20"},
        {"user21", "user21"},
        {"user22", "user22"},
        {"user23", "user23"},
        {"user24", "user24"}
    };

    public void UserLoginDetails()
    {
        var userName = userNameField.text;
        var password = passwordField.text;

        string foundPassword;
        if (loginDetails.TryGetValue(userName, out foundPassword) && foundPassword == password)
        {
            Debug.Log("User authenticated");
            loginCanvas.SetActive(false);
            userCanvas.SetActive(true);
            PlayerPrefs.SetString("username", userName);
            FindObjectOfType<GameManager>().RequestUsernameUpdate(userName);
        }
        else if (adminLoginDetails.TryGetValue(userName, out foundPassword) && foundPassword == password)
        {
            Debug.Log("Admin authenticated");
            loginCanvas.SetActive(false);
            adminCanvas.SetActive(true);
            PlayerPrefs.SetString("admin", userName);
            FindObjectOfType<GameManager>().RequestUsernameUpdate(userName);
        }

        else
        {
            errorText.gameObject.SetActive(true);
            Debug.Log("Invalid password");
        }
    }
}