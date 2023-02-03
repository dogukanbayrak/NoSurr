using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Distance Settings")]
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private float camSpeed;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - mainCharacter.transform.position;   // kamera ile ana obje arasýndaki mesafeyi offsete kaydediyor 
    }

    // Update is called once per frame
    void Update()
    {
        

        transform.position = Vector3.Slerp(transform.position, mainCharacter.transform.position + offset, camSpeed*Time.deltaTime);
        // ana obje ile kamera arasýndaki mesafeyi offseti de ekleyerek sabit tutuyor
    }
}
