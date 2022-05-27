using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
    public Animator anim;

    public void loadScene(string sceneToLoad) {
        SceneManager.LoadScene(sceneToLoad);
    } 
}
