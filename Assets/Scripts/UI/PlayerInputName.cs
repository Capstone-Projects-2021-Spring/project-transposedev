using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInputName : MonoBehaviour
{
    public GameObject PlayerInputNamePanel = null;
    public InputField nameInputField = null;
    public Button continueButton = null;
    public GameObject MultiPlayerMenuPanel = null;
    public GameObject SinglePlayerMenuPanel = null;

    public static string DisplayName { get; private set; }
    private string PlayerPrefsNameKey = null;

    private void SetUpInputField()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey))
        {
            return;
        }
        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
        nameInputField.text = defaultName;
        setPlayerName();
    }
    public void setPlayerName()
    {
        continueButton.interactable = !string.IsNullOrEmpty(nameInputField.text);
    }

    public void SavePlayerName()
    {
        DisplayName = nameInputField.text;
        PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
        
        
        PlayerInputNamePanel.SetActive(false);
        if (MainMenu.isMultiPlayer())
        {
            Debug.Log("MultiPlayer Name: " + DisplayName);
            MultiPlayerMenuPanel.SetActive(true);

        }
        else
        {
            Debug.Log("SinglePlayer Name: " + DisplayName);
            //SinglePlayer Menu
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        continueButton.onClick.AddListener(SavePlayerName);
        SetUpInputField();
    }

    // Update is called once per frame
    void Update()
    {
        setPlayerName();
    }
}
