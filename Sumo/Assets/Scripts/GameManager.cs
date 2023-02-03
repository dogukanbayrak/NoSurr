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
        SetEnemyList();  // T�m yar����lar� listeye ekler

        
    }
    private void Start()
    {
        Time.timeScale = 0;
        activePlayerCount= enemyList.Count;
        
    }

    // Update is called once per frame
    void Update()
    {
        activePlayerCount = enemyList.Count;   // E�er oyuncu �l�rse diye kontrol

        if ( time<0)  // oyun biti�i �artlar�
        {
            endGame();
        }
        else if (activePlayerCount == 1)
        {
            winGame();
        }

        time -= Time.deltaTime;  // ekrandaki s�re de�eri

    }

    // T�m enemy tag�na sahip objeleri listede tutar

    private void SetEnemyList()
    {

        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObject in enemyArray)
        {
            enemyList.Add(gameObject);
        }
    }

    // oyun biti� fonksiyonu
    void endGame()
    {
        Time.timeScale = 0f;
        uIController.GetComponent<UIController>().endScreen.gameObject.SetActive(true);
        uIController.GetComponent<UIController>().pauseButton.gameObject.SetActive(false);

    }
    void winGame()
    {
        Time.timeScale = 0f;
        uIController.GetComponent<UIController>().winScreen.gameObject.SetActive(true);
        uIController.GetComponent<UIController>().pauseButton.gameObject.SetActive(false);
    }

}
