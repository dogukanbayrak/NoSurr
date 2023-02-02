using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Distance Settings")]
    public GameObject mainCharacter;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - mainCharacter.transform.position;   // kamera ile ana obje arasındaki mesafeyi offsete kaydediyor 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCharacter.transform.position + offset;  // ana obje ile kamera arasındaki mesafeyi offseti de ekleyerek sabit tutuyor
    }
}
