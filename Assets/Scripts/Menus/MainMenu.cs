using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameJoltUIScript gamej = new GameJoltUIScript();

    void Start() {
        DiscordController.ChangeActivity("Main Menu", "asign", "In Main Menu");
    }

    public void PlayGame() {
        SceneManager.LoadScene("TestLevel");
        DiscordController.ChangeActivity("Playing", "gamelogo", "Playing");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void GameJolt() {
        SceneManager.LoadScene("GameJoltUI");
        DiscordController.ChangeActivity("GameJolt Login", "gamejolt", "GameJolt Login Screen");
    }

    public void CheckPlayerLogin() {
        if (!gamej.playerLoggedIn) {
            gamej.SendNotification("Login");
        }
    }
}
