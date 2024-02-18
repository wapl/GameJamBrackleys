using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource ;
    public AudioClip gameMusic;
    public Boolean Gameover = false; 
    [SerializeField] GameObject powerUps;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.loop = true;
        audioSource.clip = gameMusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    public void openDialog()
    {
        powerUps.gameObject.SetActive(true);
    }
    public void closeDialog()
    {
        powerUps.gameObject.SetActive(false);
    }
}
