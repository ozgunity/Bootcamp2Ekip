using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//buras� olmadan di�er sahneye ge�mez

public class Menuscript : MonoBehaviour
{

    public void PlayGame(string nextscene)//sonraki sahneye ge�i�  
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(nextscene);
    }
    public void ExitGame()
    {
        Application.Quit();
        //Debug.Log("Exit Game");
    }
}
