using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);  
    }

    public void ExitGame() {
        Application.Quit();
    }
}
