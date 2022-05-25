using UnityEngine;
using TMPro;

public class DebugDisplay : MonoBehaviour {
    public TextMeshProUGUI DebugText;

    // Framerate
    public float updateTime = 0.5f;
    private float time;
    private int frameCount;

    // Player Pos
    private GameObject playerObj;
    private string playerPos;
    private string dashStatus;

    void Start() {
        DebugText.gameObject.SetActive(false);
    }

    void Update() {
        time += Time.deltaTime;

        frameCount++;

        if (time >= updateTime) {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            playerObj = GameObject.FindGameObjectWithTag("Player");
            playerPos = ("X " + playerObj.transform.position.x + " Y " + playerObj.transform.position.y);

            dashStatus = ("Always Ready");

            DebugText.text = ("FPS: " + frameRate.ToString() + "\nPos: " + playerPos + "\nDash: " + dashStatus);

            time -= updateTime;
            frameCount = 0;
        }

        if (DebugController.debug_visible == false) {
            DebugText.gameObject.SetActive(false);
        } else {
            DebugText.gameObject.SetActive(true);
        }
    }
}
