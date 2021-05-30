using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;        //单例模式

    private PlayerController player;
    private Door doorExit;
    public bool gameOver;
    public List<Enemy> enemyList = new List<Enemy>();
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //player = FindObjectOfType<PlayerController>();
        //doorExit = FindObjectOfType<Door>();
    }
    private void Update()
    {
        if(player != null)
            gameOver = player.isDead;
        if(UIManager.instance !=null)
            UIManager.instance.GameOverUI(gameOver);
    }
    
    public void IsPlayer(PlayerController controller)   //观察者模式赋值
    {
        player = controller;
    }

    public void IsExitDoor(Door door)
    {
        doorExit = door;
    }
    
    public void IsEnemy(Enemy enemy)        
    {
        enemyList.Add(enemy);       //添加敌人进列表
    }
    public void EnemyDead(Enemy enemy)
    {
        enemyList.Remove(enemy);
        if(enemyList.Count == 0 && doorExit!=null)
        {
            doorExit.OpenDoor();
            SaveData();
        }
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();        //清楚所有保存的数据和键值
        SceneManager.LoadScene(1);
    }
    public void Coutinue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("sceneIndex"));
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.DeleteKey("playerHealth");
    }
    public void NextScene()
    {
        if (PlayerPrefs.HasKey("sceneIndex"))
            SceneManager.LoadScene(PlayerPrefs.GetInt("sceneIndex"));
        else
            NewGame();
        //Debug.Log("没数据");

    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public float LoadHealth()       //使用PlayerPrefs传递现在的血量到下一个场景
    {
        if (!PlayerPrefs.HasKey("playerHealth"))
        {
            PlayerPrefs.SetFloat("playerHealth", 3f);
        }
        float currentHealth = PlayerPrefs.GetFloat("playerHealth");

        return currentHealth;
    }
    public void SaveData()          //保存数据
    {
        PlayerPrefs.SetFloat("playerHealth", player.health);
        PlayerPrefs.SetInt("sceneIndex", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.Save();
    }
}
