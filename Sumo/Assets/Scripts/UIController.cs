using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject pauseScreen;
    public GameObject endScreen;
    public GameObject pauseButton;
    [SerializeField] private GameObject gameManager;
    public bool endGameCheck=false;
    
    [SerializeField] private int maxPlayer;

    [SerializeField] private TextMeshProUGUI activePlayerText;
    [SerializeField] private TextMeshProUGUI timeText;


    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        maxPlayer= gameManager.GetComponent<GameManager>().enemyList.Count;
    }

   
    void Update()
    {
        activePlayerText.text = "Players: "+ gameManager.GetComponent<GameManager>().activePlayerCount + " / " +maxPlayer;  // Sol �stteki oyuncu say�s�

        int timeFixer = (int)gameManager.GetComponent<GameManager>().time;  // time float de�erini int yapar

        timeText.text = "Time: "+ timeFixer.ToString();  // ekrana s�reyi yazd�r�r

    }

    public void PlayButton()
    {
        Time.timeScale = 1.0f;
        menuScreen.gameObject.SetActive(false);

    }
    public void PauseButton()
    {
        Time.timeScale = 0f;
        pauseScreen.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);

    }
    public void ResumeButton()
    {
        Time.timeScale = 1f;
        pauseScreen.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);

    }


}
