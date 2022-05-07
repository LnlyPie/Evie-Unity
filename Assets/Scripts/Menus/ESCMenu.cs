using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCMenu : MonoBehaviour
{
    public GameObject escMenu;
    public static GameObject escMenuS;
    public static bool menuOpen = false;

    void Start() {
        escMenuS = escMenu;
        HideMenu();
        menuOpen = false;
    }

    public static void ESCMenuCheck() {
        if (menuOpen == false) {
            ShowMenu();
        } else {
            HideMenu();
        }
    }

    public void Resume() {
        HideMenu();
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
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
