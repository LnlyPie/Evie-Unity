using GameJolt.API;
using GameJolt.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GJUIScript {
	public class GameJoltUIScript : MonoBehaviour {
		public Button ShowTrophiesButton;

		public void AutoLoginCallback(AutoLoginResult result) {
			Debug.Log(string.Format("Auto login result: {0}", result));
			DownloadAvatar();
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

		public void SendNotification() {
			GameJoltUI.Instance.QueueNotification(
				string.Format("Notification <b>Test</b>"));
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
}