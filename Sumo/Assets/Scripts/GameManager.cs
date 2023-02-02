using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    private GameObject[] enemyArray;

    
    void Awake()
    {
        SetEnemyList();  // Tüm yarýþçýlarý listeye ekler

        
    }
    private void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetEnemyList()
    {
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObject in enemyArray)
        {
            enemyList.Add(gameObject);
        }
    }
}
