using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //private NetworkManagerLobby networkManager = null;
    public GameObject PlayerInputNamePanel = null;
    public GameObject JoinLobbyMenuPanel = null;
    public GameObject MainMenuPanel = null;
    public GameObject MultiPlayerMenuPanel = null;
    public Button Button_SP = null;
    public Button Button_MP = null;
    public Button Button_Quit = null;
    private static bool isMP = false;
    void Start()
    {
        //networkManager.StartHost();
        PlayerInputNamePanel.SetActive(false);
        JoinLobbyMenuPanel.SetActive(false);
        MultiPlayerMenuPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        Button_SP.onClick.AddListener(OnClickSinglePlayer);
        Button_MP.onClick.AddListener(OnClickMultiPlayer);
        Button_Quit.onClick.AddListener(OnClickQuit);
    }

    public void ActiveMainMenu(bool active)
    {
        MainMenuPanel.SetActive(active);
    }
    public static bool isMultiPlayer()
    {
        return isMP;
    }
    void OnClickSinglePlayer()
    {
        MainMenuPanel.SetActive(false);
        //SceneManager.LoadScene("TestingPlayerController");
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);

        
        //for future
        //PlayerInputNamePanel.SetActive(true);
        
        MainMenu.isMP = false;

    }
    void OnClickMultiPlayer()
    {
        PlayerInputNamePanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        MainMenu.isMP = true;
    }
    void OnClickQuit()
    {
        QuitGame();
    }
    void QuitGame()
    {
        Application.Quit();
    }
}
