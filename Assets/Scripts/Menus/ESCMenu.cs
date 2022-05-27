using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCMenu : MonoBehaviour
{
    SceneHandler scene = new SceneHandler();

    public GameObject escMenu;
    public static GameObject escMenuS;
    public static bool menuOpen = false;

    void Start() {
        escMenuS = escMenu;
        HideMenu();
        menuOpen = false;
    }

    void Update() {
        if ( (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7")) ) {
            if (!menuOpen) {
                ShowMenu();
            } else {
                HideMenu();
            }
        }
    }

    public void Resume() {
        HideMenu();
    }

    public void MainMenu() {
        scene.loadScene("MainMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }

    static void ShowMenu() {
        menuOpen = true;
        escMenuS.gameObject.SetActive(true);
        Time.timeScale = 0;
        DiscordController.ChangeActivity("Paused", "asign", "Paused");
        if (!DebugController.cursor_visible) {
            DebugController.cursor_visible = true;
        }
    }

    static void HideMenu() {
        menuOpen = false;
        escMenuS.gameObject.SetActive(false);
        Time.timeScale = 1;
        DiscordController.ChangeActivity("Playing", "gamelogo", "Playing");
        if (DebugController.cursor_visible) {
            DebugController.cursor_visible = false;
        }
    }
}
