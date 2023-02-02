using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [Header("Movement Settings")]
    Rigidbody rb;
    private CharacterController characterController;
    [SerializeField] private float inputX,inputZ;
    [SerializeField] private Vector3 movement,velocity;
    [SerializeField] private float moveSpeed,gravity;
    [SerializeField] private float kickPower;


    [Header("Enemy Settings")]
    [SerializeField] private GameObject gameManager;
    [SerializeField] private List<GameObject> charaterEnemyList = new List<GameObject>();
    






    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        rb = GetComponent<Rigidbody>();
        GetEnemyList();
        characterController = GetComponent<CharacterController>();

        


    }

    // Update is called once per frame
    void Update()
    {
        Movement();
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
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("111");
            Rigidbody otherRB = collision.gameObject.GetComponent<Rigidbody>();
            otherRB.AddForce(0, 0, 10, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Rigidbody otherRB=other.gameObject.GetComponent<Rigidbody>();
            otherRB.AddForce(0, 0, kickPower,ForceMode.Impulse);

        }
    }


}
