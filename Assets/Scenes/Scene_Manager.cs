using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour {
    public void LoadLevel(int LevelNumber) {
        string NameScene = "Level_" + (LevelNumber).ToString();
        SceneManager.LoadScene(NameScene);
    }
    public void ExitMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame() {
        Application.Quit();
    }
}