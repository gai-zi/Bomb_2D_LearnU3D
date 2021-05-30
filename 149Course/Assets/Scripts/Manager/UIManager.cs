using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//！！！使用单例模式
public class UIManager : MonoBehaviour
{
    public static UIManager instance;       //只有这个唯一对象
    public GameObject healthBar;

    [Header("UI Elements")]
    public GameObject pauseMenu;
    public Slider BossHealthBar;
    public GameObject gameOverPanel;


    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateHealth(float currentHealth)
    {
        switch (currentHealth)
        {
            case 3:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(true);
                healthBar.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 2:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(true);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(false);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 0:
                healthBar.transform.GetChild(0).gameObject.SetActive(false);
                healthBar.transform.GetChild(1).gameObject.SetActive(false);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);

        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1;
    }
    public void SetBossHealth(float health)
    {
        BossHealthBar.maxValue = health;
    }
    public void UpdateBossHealth(float health)
    {
        BossHealthBar.value = health;
    }
    public void GameOverUI(bool playerDead)
    {
        gameOverPanel.SetActive(playerDead);
    }
}
