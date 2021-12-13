using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverBehavior : MonoBehaviour
{
    public void EndGame()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }

    public void RestartGame(){
        Time.timeScale = 1;
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
    }
}
