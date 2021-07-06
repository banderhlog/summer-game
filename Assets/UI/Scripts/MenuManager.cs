using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour{
    private GameObject PauseMenu;
    private GameObject BackgroundDeathScreen;
    private GameObject Continue;
    private GameObject Restart;
    private GameObject ExitPause;
    private GameObject ExitDied;
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        BackgroundDeathScreen = GameObject.Find("PauseMenu/BackgroundDeathScreen"); BackgroundDeathScreen.SetActive(false);
        Continue              = GameObject.Find("PauseMenu/Continue");
        Restart               = GameObject.Find("PauseMenu/Restart");               Restart.SetActive(false);
        ExitPause             = GameObject.Find("PauseMenu/ExitPause");
        ExitDied              = GameObject.Find("PauseMenu/ExitDied");              ExitDied.SetActive(false);
        PauseMenu             = GameObject.Find("PauseMenu");                       PauseMenu.SetActive(false);
        EndPause();//Если нажать паузу, выйти и начать игру, то она начнется в режиме паузы, эта штука снимает ее
    }
    public void StartPause() {
        PauseMenu.SetActive(true);
          Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;
    }
    public void EndPause() {
        PauseMenu.SetActive(false);
          Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void PlayerIsDied() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenu.SetActive(true);
        BackgroundDeathScreen.SetActive(true);
        Restart.SetActive(true);
        ExitDied.SetActive(true);
        Continue.SetActive(false);
        ExitPause.SetActive(false);
    }
    public void HideDeathScreen() {
        Cursor.lockState = CursorLockMode.Locked;
        BackgroundDeathScreen.SetActive(false);
        Restart.SetActive(false);
        ExitDied.SetActive(false);
        Continue.SetActive(true);
        ExitPause.SetActive(true);
        PauseMenu.SetActive(false);
    }
}
