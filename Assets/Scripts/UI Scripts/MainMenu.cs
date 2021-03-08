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

    // Start is called before the first frame update
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
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnClickSinglePlayer()
    {
        Debug.Log("You have clicked the Single Player button!");
        MainMenuPanel.SetActive(false);
        //SceneManager.LoadScene("TestingPlayerController");
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);

        /*future
        PlayerInputNamePanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        MainMenu.isMP = false;
        */
    }
    void OnClickMultiPlayer()
    {
        Debug.Log("You have clicked the MultiPlayer button!");
        PlayerInputNamePanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        MainMenu.isMP = true;
    }
    void OnClickQuit()
    {
        Debug.Log("You have clicked the Quit button!");
        QuitGame();
    }
    void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
        //Just to make sure its working
    }
}
