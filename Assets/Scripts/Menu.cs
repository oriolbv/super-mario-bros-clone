using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameScore.Instance.initGameLives();
            SceneManager.LoadScene("LevelMenuScene");
        }
    }
}
