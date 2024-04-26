using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource pop;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        Debug.Log("GAME MANAGER AWAKE");
    }

    private void Start()
    { 

        //mute toggle player pref
        if (PlayerPrefs.HasKey("muted"))
        {
            int muteValue = PlayerPrefs.GetInt("muted");

            if (muteValue == 1 && UIManager.Instance != null)
            {
                UIManager.Instance.ToggleMute();
            }
        }

    }
    public void Play()
    {
        //On Play Button Click -- Load Main Scene 
        SceneManager.LoadScene(1);
        Debug.Log("Play Button");
    }
    public void Restart()
    {
        //on restart button click
        SceneManager.LoadScene(1);
        Debug.Log("Restart Button");
    }
    public void Quit()
    {
        //on quit button click
        Application.Quit();
    }

    public void WinGame()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.EnableGameWinCanvas();
        }
         
    }
}
