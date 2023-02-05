using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void Quit()
    {
       Application.Quit();
    }
}
