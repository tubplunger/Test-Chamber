using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerMoney = 0;

    public GameObject gameOverPanel;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateMoney(playerMoney);
        }
    }

    public void AddMoney(int amount)
    {
        playerMoney += amount;
        
        Debug.Log("Money: " + playerMoney);

        if (UIManager.instance != null)
        {
            UIManager.instance.UpdateMoney(playerMoney);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
}
