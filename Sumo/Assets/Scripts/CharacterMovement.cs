using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{

    [Header("Movement Settings")]
    Rigidbody rb;
    private CharacterController characterController;
    [SerializeField] private float inputX,inputZ;
    [SerializeField] private Vector3 movement,velocity;
    [SerializeField] private float moveSpeed,gravity;
    [SerializeField] private float kickPower;
    [SerializeField] private float torkPower;


    [Header("Enemy Settings")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private List<GameObject> charaterEnemyList = new List<GameObject>();
    






    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        rb = GetComponent<Rigidbody>();
       // GetEnemyList();
        characterController = GetComponent<CharacterController>();

        


    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
        // Movement2();
        Movement4();
    }

    private void FixedUpdate()
    {
       // Movement3();
    }


    void Movement()
    {

        //float rotate = Input.GetAxis("Horizontal");  // dönüþ açýsý hesaplama


        //direction = new Vector3(0, 0, Input.GetAxis("Vertical")*speed*Time.deltaTime);   // ileri yön vektörü

        ////rb.MovePosition(rb.position + direction * Time.deltaTime * speed);  // ileri hareket



        //transform.localPosition += direction;

        //gameObject.transform.localRotation= Quaternion.Euler(new Vector3(0f, rotate * turnSpeed, 0f)); ;  // dönme açýsý


        if (characterController.isGrounded)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y -= gravity*Time.deltaTime;
        }


        inputX = Input.GetAxis("Horizontal");
        inputZ= Input.GetAxis("Vertical");

        //forward movement
        movement = characterController.transform.forward * inputZ;

        //character rotate
        characterController.transform.Rotate(Vector3.up*inputX*(Time.deltaTime*100f));

        // character movement

        characterController.Move(movement * moveSpeed * Time.deltaTime);
        characterController.Move(velocity);



    }


    void Movement2()
    {
        //rb.angularVelocity = Vector3.zero;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        transform.Rotate(Vector3.up * x * (Time.deltaTime * 100f));

        Vector3 move = new Vector3(0, 0, z) * Time.deltaTime * moveSpeed;

        
        transform.localPosition += move;

        //rb.MovePosition(transform.localPosition + transform.TransformDirection(move));




    }

    void Movement3()
    {
        float h= Input.GetAxis("Horizontal")*Time.deltaTime*50;
        float v = Input.GetAxis("Horizontal")* Time.deltaTime*50;
        rb.AddTorque(transform.up * h*torkPower);

        Vector3 move = new Vector3(0, 0, v) * Time.deltaTime * moveSpeed;
        rb.MovePosition(transform.position + transform.TransformDirection(move));

    }

    void Movement4()
    {
        float x = Input.GetAxis("Horizontal")*Time.deltaTime* moveSpeed;
        float z = Input.GetAxis("Vertical")*Time.deltaTime* moveSpeed;
        Vector3 direction = new Vector3(x, 0, z);
        transform.position += direction;
        
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
