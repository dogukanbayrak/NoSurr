using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    private GameObject[] enemyArray;

    public int activePlayerCount;

    [SerializeField] private GameObject uIController;
    public float time;


    void Awake()
    {
        SetEnemyList();  // Tüm yarýþçýlarý listeye ekler

        
    }
    private void Start()
    {
        Time.timeScale = 0;
        activePlayerCount= enemyList.Count;
        
    }

    // Update is called once per frame
    void Update()
    {
        activePlayerCount = enemyList.Count;

        if (activePlayerCount ==1 || time<0)
        {
            endGame();


        }

        time -= Time.deltaTime;

    }

    private void SetEnemyList()
    {

        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObject in enemyArray)
        {
            enemyList.Add(gameObject);
        }
    }


    void endGame()
    {
        Time.timeScale = 0f;
        uIController.GetComponent<UIController>().endScreen.gameObject.SetActive(true);
        uIController.GetComponent<UIController>().pauseButton.gameObject.SetActive(false);


    }
}
