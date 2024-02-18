using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpButton : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip highlighted;
    public AudioClip clicked;
    public GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playHighlight()
    {
        
        audioSource.clip = highlighted;
        audioSource.Play();
    }
    public void playClicked()
    {
        audioSource.clip = clicked;
        audioSource.Play();
    }
}
