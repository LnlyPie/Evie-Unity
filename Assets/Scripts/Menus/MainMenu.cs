using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameJoltUIScript gamej = new GameJoltUIScript();

    public void PlayGame() {
        SceneManager.LoadScene("TestLevel");
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void GameJolt() {
        SceneManager.LoadScene("GameJoltUI");
    }

    public void BugsBtn() {
        SceneManager.LoadScene("Bugs");
    }

    public void CheckPlayerLogin() {
        if (!gamej.playerLoggedIn) {
            gamej.SendNotification("Login");
        }
    }
}
