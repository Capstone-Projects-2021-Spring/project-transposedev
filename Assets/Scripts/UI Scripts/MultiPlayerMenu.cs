using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlayerMenu : MonoBehaviour
{
    public Button Button_Host = null;
    public Button Button_Join = null;
    public Button Button_PrePage = null;
    public GameObject MultiPlayerMenuPanel = null;
    public GameObject IPInputPanel = null;
    public GameObject MainMenuPanel = null;
    public void IPInputMenu()
    {

        IPInputPanel.SetActive(true);
        MultiPlayerMenuPanel.SetActive(false);

    }
    public void HostMenu()
    {

    //host

    }
    public void MainMenuPage()
    {
        MultiPlayerMenuPanel.SetActive(false);
        IPInputPanel.SetActive(false);
        MainMenuPanel.SetActive(true);

    }
    void Start()
    {
        Button_Join.onClick.AddListener(IPInputMenu);
        Button_Host.onClick.AddListener(HostMenu);
        Button_PrePage.onClick.AddListener(MainMenuPage);
    }
}
