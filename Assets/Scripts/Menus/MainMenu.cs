using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameJoltUIScript gamej = new GameJoltUIScript();
    SceneHandler scene = new SceneHandler();

    void Start() {
        DiscordController.ChangeActivity("Main Menu", "asign", "In Main Menu");
    }

    public void PlayGame() {
        scene.loadScene("TestLevel");
        DiscordController.ChangeActivity("Playing", "gamelogo", "Playing");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void GameJolt() {
        scene.loadScene("GameJoltUI");
        DiscordController.ChangeActivity("GameJolt Login", "gamejolt", "GameJolt Login Screen");
    }

    public void CheckPlayerLogin() {
        if (!gamej.playerLoggedIn) {
            gamej.SendNotification("Login");
        }
    }
}
