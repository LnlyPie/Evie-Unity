using GameJolt.API;
using GameJolt.UI;
using UnityEngine;
using UnityEngine.UI;

public class GameJoltUIScript : MonoBehaviour {
	public Button ShowTrophiesButton;
	private int notificationQueued;

	public void AutoLoginCallback(AutoLoginResult result) {
		Debug.Log(string.Format("Auto login result: {0}", result));
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
			DownloadAvatar();
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

	public void QueueNotification() {
		GameJoltUI.Instance.QueueNotification(
			string.Format("Notification <b>#{0}</b>", ++notificationQueued));
	}

	public void ShowLeaderboards() {
		GameJoltUI.Instance.ShowLeaderboards();
	}

	public void Pause() {
		Time.timeScale = 0f;
	}

	public void Resume() {
		Time.timeScale = 1f;
	}
}