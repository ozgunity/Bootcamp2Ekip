using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//burasý olmadan diðer sahneye geçmez

public class Menuscript : MonoBehaviour
{

    public void PlayGame(string nextscene)//sonraki sahneye geçiþ  
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
