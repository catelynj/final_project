using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text targetText;
    public Canvas optionsCanvas;
    public Canvas gameOverCanvas;
    public Canvas gameWinCanvas;

    private UnityAction enableOptions;
    public static UIManager instance;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);


        //not necessary to use UnityAction here tbh but i wanted to try it
        enableOptions = new UnityAction(EnableOptions);
        
    }
    void Start()
    {
        //hide canvases on start
        optionsCanvas.enabled = false;
        gameOverCanvas.enabled = false;
        gameWinCanvas.enabled = false;

        targetText.text = " " + GameManager.instance.target;
    }

    void Update()
    {
        scoreText.text = " " + GameManager.instance.score;
    }

    public void EnableOptions()
    {
        optionsCanvas.enabled = true;
    }

    public void DisableOptions()
    {
        optionsCanvas.enabled = false;
    }

    public void EnableGameOverCanvas()
    {
        gameOverCanvas.enabled = true;
    }

    public void EnableGameWinCanvas()
    {
        gameWinCanvas.enabled = true;
    }

    //Note: this could probably be in GameManager rather than UIManager but it uses a button so i put it here
    public void Play()
    {
        //On Play Button Click -- Load Main Scene 
        SceneManager.LoadScene(1);
    }

    private void OnEnable()
    {
        EventManager.StartListening("EnableOptions", enableOptions);
    }
    private void OnDisable()
    {
        EventManager.StopListening("EnableOptions", enableOptions);
    }
}
