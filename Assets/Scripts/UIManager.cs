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
    public Canvas optionsPanel;

    private UnityAction enableOptions;

    private void Awake()
    {
        enableOptions = new UnityAction(EnableOptions);
    }
    void Start()
    {
        //hide options panel on start
        optionsPanel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = " " + GameManager.instance.score;
    }

    public void EnableOptions()
    {
        optionsPanel.enabled = true;
    }

    public void DisableOptions()
    {
        optionsPanel.enabled = false;
    }

    public void Play()
    {
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
