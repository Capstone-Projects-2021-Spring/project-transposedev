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
    // Start is called before the first frame update
    void Start()
    {
        inEsc = true;
        Button_Return.onClick.AddListener(OnClickReturn);
        Button_Quit.onClick.AddListener(OnClickQuit);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("You have closed the Esc Menu!");
            inEsc = false;
            SceneManager.UnloadScene("EscMenu");
        }
    }
    void OnClickReturn()
    {
        Debug.Log("You have clicked the Return button!");
        inEsc = false;
        SceneManager.UnloadScene("EscMenu");
        //resume the game
    }
    void OnClickQuit()
    {
        Debug.Log("You have clicked the Quit button!");
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
    public static bool isInEscMenu()
    {
        return inEsc;
    }
}
