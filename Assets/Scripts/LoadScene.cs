using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("GameScenebackup");
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Win()
    {
        SceneManager.LoadScene("Win");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
