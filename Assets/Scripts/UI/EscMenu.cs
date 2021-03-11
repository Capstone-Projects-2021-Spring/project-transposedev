using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    private static bool inEsc = false;

    public Button Button_Return = null;
    public Button Button_Quit = null;
    void Start()
    {
        inEsc = true;
        Button_Return.onClick.AddListener(OnClickReturn);
        Button_Quit.onClick.AddListener(OnClickQuit);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inEsc = false;
            SceneManager.UnloadScene("EscMenu");
        }
    }
    void OnClickReturn()
    {
        inEsc = false;
        SceneManager.UnloadScene("EscMenu");
        //resume the game
    }
    void OnClickQuit()
    {
        QuitGame();
    }
    void QuitGame()
    {
        //back to menu & if multiplayer also disconnected
        inEsc = false;
        //SceneManager.UnloadScene("TestingPlayerController");
        SceneManager.UnloadScene("HUD");
        SceneManager.UnloadScene("EscMenu");
        SceneManager.LoadScene("Menu");
    }
    public static bool isInEscMenu()//use this to check if client is in Esc menu
    {
        return inEsc;
    }
}
