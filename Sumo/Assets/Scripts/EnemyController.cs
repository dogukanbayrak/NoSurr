using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject gameManager;

    [SerializeField] private List<GameObject> characterEnemyList = new List<GameObject>();
    [SerializeField] private int listFixer;

    private NavMeshAgent navMeshAgent;
    [SerializeField] private float kickPower;
    [SerializeField] private GameObject closestEnemy;

    // Start is called before the first frame update
    void Start()
    {

        
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        SetEnemyList();
        navMeshAgent = GetComponent<NavMeshAgent>();
        listFixer = gameManager.GetComponent<GameManager>().activePlayerCount;

    }

    // Update is called once per frame
    void Update()
    {

        if(listFixer != gameManager.GetComponent<GameManager>().activePlayerCount)
        {
            SetEnemyList();
            listFixer = gameManager.GetComponent<GameManager>().activePlayerCount;
        }

        closestEnemy = ClosestEnemy();
        MoveToTheClosestObject();
    }

    void SetEnemyList()
    {
        characterEnemyList.Clear();
        foreach (GameObject x in gameManager.GetComponent<GameManager>().enemyList)
        {
            characterEnemyList.Add(x);

            if (x.GetInstanceID() == gameObject.GetInstanceID())
            {
                characterEnemyList.Remove(x);
            }

        }
    }
    

    // En yakýn rakibi tespit eder
    GameObject ClosestEnemy()
    {

        GameObject closestHere = gameObject;
        float leastDistance = Mathf.Infinity;

        foreach (var enemy in characterEnemyList)
        {

            float distanceHere = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceHere < leastDistance)
            {
                leastDistance = distanceHere;
                closestHere = enemy;
            }

        }
        return closestHere;
    }


    // Rakip ile temasa girerse ona kuvvet uygular

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("222");
            Rigidbody otherRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 dir = (collision.transform.position-gameObject.transform.position).normalized;  // kuvvet yönü hesaplama

            otherRB.AddForce(new Vector3(dir.x,0,dir.z)*kickPower, ForceMode.Impulse);  // kuvvet uygulandýðý an
        }
    }

    // Eðer harita dýþýna çýkarsa Dead isimli gameobjeye çarpar ve fonksiyon tetiklenir
    // obje yok edilir ve gamemanager listesinden kendisini çýkartýr
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dead")
        {
            Debug.Log("dead");
            Destroy(gameObject);

            foreach (GameObject x in gameManager.GetComponent<GameManager>().enemyList)
            {
                
                if (x.GetInstanceID() == gameObject.GetInstanceID())
                {
                    gameManager.GetComponent<GameManager>().enemyList.Remove(x);
                }
            }

            


        }
    }

    // navmesh kullanarak en yakýn rakib objesine hareket eder
    void MoveToTheClosestObject()
    {
        navMeshAgent.destination = closestEnemy.transform.position;
        
    }
}
    