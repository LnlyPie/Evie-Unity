using GameJolt.API;
using GameJolt.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameJoltUIScript : MonoBehaviour {
	public Button ShowTrophiesButton;
	public bool playerLoggedIn = false;

	SceneHandler scene = new SceneHandler();

	public void AutoLoginCallback(AutoLoginResult result) {
		Debug.Log(string.Format("Auto login result: {0}", result));
		DownloadAvatar();
		playerLoggedIn = true;
	}

	public void SignInButtonClicked() {
		GameJoltUI.Instance.ShowSignIn(signInSuccess => {
			if(signInSuccess) {
				ShowTrophiesButton.interactable = true;
				Debug.Log("Logged In");
			} else {
				Debug.Log("Dismissed or Failed");
			}
		}, userFetchSuccess => {
			Debug.Log(string.Format("User's Information Fetch {0}.", userFetchSuccess ? "Successful" : "Failed"));
			if (userFetchSuccess == true) {
				DownloadAvatar();
				SendNotification("Sign In Successful");
				playerLoggedIn = true;
				UnlockTrophy(158932);
			}
		});
	}

	public void SignOutButtonClicked() {
		if(GameJoltAPI.Instance.HasUser) {
			ShowTrophiesButton.interactable = false;
			GameJoltAPI.Instance.CurrentUser.SignOut();
		}
	}

	public void DownloadAvatar() {
		GameJoltAPI.Instance.CurrentUser.DownloadAvatar(success =>
			Debug.LogFormat("Downloading avatar {0}", success ? "succeeded" : "failed"));
	}

	public void SendNotification(string text) {
		GameJoltUI.Instance.QueueNotification(
			string.Format(text));
	}

	public void ShowLeaderboards() {
		GameJoltUI.Instance.ShowLeaderboards();
	}

	public void ShowTrophies() {
		GameJoltUI.Instance.ShowTrophies();
	}

	public void GoBack() {
		scene.loadScene("MainMenu");
	}

	public void Pause() {
		Time.timeScale = 0f;
	}

	public void Resume() {
		Time.timeScale = 1f;
	}

	public void UnlockTrophy(int id) {
		GameJolt.API.Trophies.TryUnlock(id, (TryUnlockResult result) => {
      switch(result) {
        case TryUnlockResult.Unlocked:
          Debug.Log("You've unlocked the trophy!");
          break;
        case TryUnlockResult.AlreadyUnlocked:
          Debug.Log("The trophy was already unlocked");
          break;
        case TryUnlockResult.Failure:
          Debug.LogError("Something went wrong!");
          break;
			}
		});
	}
}