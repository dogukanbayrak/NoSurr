using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{

    [Header("Movement Settings")]
    Rigidbody rb;
    [SerializeField] private float inputX,inputZ;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float kickPower;

    [SerializeField] private Joystick joy;



    [Header("Enemy Settings")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private List<GameObject> charaterEnemyList = new List<GameObject>();
    






    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        rb = GetComponent<Rigidbody>();
       // GetEnemyList();
        

        


    }

    // Update is called once per frame
    void Update()
    {
        
        Movement();
    }



    void Movement()
    {
        float x = Input.GetAxis("Horizontal")*Time.deltaTime* moveSpeed;
        float z = Input.GetAxis("Vertical")*Time.deltaTime* moveSpeed;

        float joyHorizantalMove = joy.Horizontal * moveSpeed * Time.deltaTime;
        float verticalalMove = joy.Vertical * moveSpeed * Time.deltaTime;

        Vector3 joyMovement = new Vector3(joyHorizantalMove, 0, verticalalMove);

        transform.position += joyMovement;

        //Vector3 direction = new Vector3(x, 0, z);
        //transform.position += direction;

    }

    void GetEnemyList()
    {
        
        foreach (GameObject x in gameManager.GetComponent<GameManager>().enemyList)
        {
            charaterEnemyList.Add(x);

            if(x.GetInstanceID() == gameObject.GetInstanceID())
            {
                charaterEnemyList.Remove(x);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        // rakip ile temas ederse ona kuvvet uygular

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("111");
            Rigidbody otherRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 dir = (collision.transform.position - gameObject.transform.position).normalized; // kuvvetin açýsýný hesaplar

            otherRB.AddForce(new Vector3(dir.x, 0, dir.z) * kickPower, ForceMode.Impulse);  // kuvvet uygular
        }
    }

    // kullanýcý yanarsa sahne yeniden baþlatýlýr
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dead")
        {
            SceneManager.LoadScene(0);

        }
    }


}
