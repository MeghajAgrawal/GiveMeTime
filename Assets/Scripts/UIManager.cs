using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool winCondition = false;
    public GameObject GameOverUI;
    public GameObject PauseMenuUI;
    public GameObject WinConditionUI;
    public TextMeshProUGUI enemycount;

    public static bool fadeIn = false;
    public static bool fadeOut = true;
    public CanvasGroup Toasting;
    void Update()
    {
        if(winCondition)
        {
            WinConditionUI.SetActive(true);
            enemycount.text = "Total Enemies Defeated : "+ GameHub.TotalEnemies;
        }
        if(fadeIn){
            if(Toasting.alpha < 1){
                Toasting.alpha += Time.deltaTime;
                if(Toasting.alpha >= 1){
                    fadeIn = false;
                    Invoke(nameof(HideUI),1f);
                }
            }
        }
        if(fadeOut){
            if(Toasting.alpha > 0){
                Toasting.alpha -= Time.deltaTime;
                if(Toasting.alpha == 0){
                    fadeOut = false;
                }
            }
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverUI.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        GameHub.TotalEnemies = 0;
        GameOverUI.SetActive(false);
        WinConditionUI.SetActive(false);
        winCondition = false;
        SceneManager.LoadScene(1);
        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        PauseMenuUI.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenuUI.SetActive(false);
    }

    public void HideUI()
    {
        fadeOut = true;
    }

}
