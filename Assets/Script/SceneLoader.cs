using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    [SerializeField] int winScore = 4000;
    public void LoadNextScene(string sceneName)
    {
       
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void WinGame() {
        if(ScoreKeepper.score >= winScore) {
            LoadNextScene("Win Game");
        }
    }

    void Update() {

        WinGame();
    }
}
