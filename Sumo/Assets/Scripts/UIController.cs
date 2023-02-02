using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField] GameObject menuScreen;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void PlayButton()
    {
        Time.timeScale = 1.0f;
        menuScreen.gameObject.SetActive(false);

    }
    public void PauseButton()
    {
        Time.timeScale = 0f;
        menuScreen.gameObject.SetActive(true);

    }


}
