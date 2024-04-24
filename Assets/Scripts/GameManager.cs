using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource pop;
    public int target;

    public int score = 0;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        
    }

    private void Start()
    { 
        score = 0;
        target = 2000;
    }

    public void Restart()
    {
        //on restart button click
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        //on quit button click
        Application.Quit();
    }

    public void UpdateScore(int addscore)
    {
        score += addscore;
        if(score >= target)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        UIManager.instance.EnableGameWinCanvas(); 
    }
}
