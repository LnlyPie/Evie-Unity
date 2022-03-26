using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCMenu : MonoBehaviour
{
    public GameObject escMenu;
    bool menuOpen = false;

    void Start() {
        HideMenu();
        menuOpen = false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7")) {
            if (menuOpen == false) {
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
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }

    void ShowMenu() {
        menuOpen = true;
        escMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        DiscordController.ChangeActivity("Paused", "asign", "Paused");
    }

    void HideMenu() {
        menuOpen = false;
        escMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        DiscordController.ChangeActivity("Playing", "gamelogo", "Playing");
    }
}
