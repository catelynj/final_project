using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text targetText;
    public Toggle muteToggle;
    private bool isMuted;
    public Canvas optionsCanvas;
    public Canvas gameOverCanvas;
    public Canvas gameWinCanvas;

    public int target = 0;
    public int score = 0;

    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

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

        Debug.Log("AWAKE");
    }

    void Start()
    {
        Debug.Log("UIManager Start");
        //hide canvases on start
        optionsCanvas.enabled = false;

        UpdateToggleState();
        isMuted = PlayerPrefs.GetInt("muted", 0) == 1;

        if (GameManager.Instance != null && SceneManager.GetActiveScene().name == "Main")
        {
            
            gameOverCanvas.enabled = false;
            gameWinCanvas.enabled = false;
            score = 0;
            target = 2000;
            targetText.text = " " + target;
            scoreText.text = " " + score;
        }
    }
    public void UpdateScore(int addscore)
    {
        score += addscore;
        if (score >= target && Instance != null)
        {
            GameManager.Instance.WinGame();
        }
        scoreText.text = " " + score;
    }

    public void EnableOptions()
    {
        optionsCanvas.enabled = true;
        Debug.Log("CANVAS ON");
    }

    public void DisableOptions()
    {
        optionsCanvas.enabled = false;
        Debug.Log("CANVAS OFF");
    }

    public void EnableGameOverCanvas()
    {
        gameOverCanvas.enabled = true;
        Debug.Log("GAME OVER ON");

    }

    public void EnableGameWinCanvas()
    {
        gameWinCanvas.enabled = true;
        Debug.Log("GAME WIN ON");
    }

    public void ToggleMute()
    {
        Debug.Log("MUSIC TOGGLE");
        isMuted = !isMuted;

        AudioListener.volume = isMuted ? 0 : 1;

        PlayerPrefs.SetInt("muted", isMuted ? 1 : 0);
        
        
    }

    private void UpdateToggleState()
    {
        // If muteToggle is not assigned, return
        if (muteToggle == null)
            return;

        muteToggle.isOn = isMuted;
    }
}
