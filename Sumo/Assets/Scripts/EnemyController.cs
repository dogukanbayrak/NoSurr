using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject gameManager;
    [SerializeField] private List<GameObject> characterEnemyList = new List<GameObject>();


    [SerializeField] private GameObject targetObject;

    public GameObject closestEnemy;

    // Start is called before the first frame update
    void Start()
    {


        gameManager = GameObject.FindGameObjectWithTag("GameController");
        SetEnemyList();
        
    }

    // Update is called once per frame
    void Update()
    {

        closestEnemy = ClosestEnemy();
    }

    void SetEnemyList()
    {

        foreach (GameObject x in gameManager.GetComponent<GameManager>().enemyList)
        {
            characterEnemyList.Add(x);

            if (x.GetInstanceID() == gameObject.GetInstanceID())
            {
                characterEnemyList.Remove(x);
            }

        }
    }
    


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
}
    