using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
  
   public void SceneChange()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void Quit()
    {
       Application.Quit();
    }
}
