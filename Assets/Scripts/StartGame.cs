using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartTheGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastStage"));
    }
}
