using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalData : MonoBehaviour
{
    
    public GameObject quitMenu;
    public GameObject gameOver;
    public Text coinCount;
    public int coinCountInt;
    public bool paused = false;
    public bool checking = false;
    public bool loading;
    public List<GameObject> roomsSpawned;

    private void Start()
    {
        if (gameOver != null)
            gameOver.SetActive(false);
        if(quitMenu != null)
            quitMenu.SetActive(false);

    }

    public void ToGame()
    {
        SceneManager.LoadScene("RandomGen");
    }

    public void ToMenu()
    {
        paused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Closegame()
    {
        Application.Quit();
    }

    public void QuitPressed()
    {
        quitMenu.SetActive(true);
        checking = true;
    }

    public void Cancel()
    {
        quitMenu.SetActive(false);
        checking = false;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }


    private void Update()
    {
        if(coinCount !=null)
            coinCount.text = ("Coins: " + coinCountInt);
    }
}
