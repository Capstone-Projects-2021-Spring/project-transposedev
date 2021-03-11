using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IPInput : MonoBehaviour
{
    public GameObject IPInputPanel = null;
    public InputField IPInputField = null;
    public Button continueButton = null;
    public Button Button_PrePage = null;
    public GameObject MultiPlayerMenuPanel = null;

    public static string ipAddress { get; private set; }
    private string IP = null;

    public void setIP()
    {
        continueButton.interactable = !string.IsNullOrEmpty(IPInputField.text);
    }

    public void SaveIP()
    {
        ipAddress = IPInputField.text;

        //IPInputPanel.SetActive(false);
        //lobby menu


    }
    public void BackToMPMenu()
    {

        IPInputPanel.SetActive(false);
        MultiPlayerMenuPanel.SetActive(true);

    }
    void Start()
    {
        continueButton.onClick.AddListener(SaveIP);
        Button_PrePage.onClick.AddListener(BackToMPMenu);
    }

    void Update()
    {
        setIP();
    }
}
