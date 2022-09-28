using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        PlayerPrefs.SetInt("Score", 10);
        SceneManager.LoadScene("Scenes/Map");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenuScene");
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("Score", 10);
        SceneManager.LoadScene("Scenes/GameOverScene");
    }
}
