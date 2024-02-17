using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float health;
    public  int sniifferCharges = 1;
    public  int nightVisionCharges = 1;
    public AudioSource audioSource;
    public AudioClip playerHurt;
    [SerializeField] TextMeshProUGUI textScore;

    [SerializeField]private  int score;
    
    public string equppedWeapon = "";
    public Slider HealthBar; 

    public DoorsManager doorsManager;
    // Start is called before the first frame update
    void Start()
    {
        this.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sniff()
    {
        sniifferCharges--;
        
        doorsManager.playerUsedSnif = true;
    }

    public void useNightVision()
    {
        sniifferCharges--;

        doorsManager.playerUsedNightVision= true;
    }

    public void addScore(int i)
    {
       
        this.score = score + i;
        Debug.Log("Score"+this.score);
        string scoreStr = this.score.ToString();
        if(textScore!=null)
        {
            textScore.text = scoreStr;
        }
        else
        {
            Debug.Log("Is Null");
        }
       
       
    }
    public void addSniffer()
    {
        sniifferCharges++;
    }

    public void addNightVision()
    {
        nightVisionCharges++;

    }
    public void playerDamaged(float damage)
    {
        health = health - damage;
        //audioSource.clip = playerHurt;
        //audioSource.Play();
        HealthBar.value = health / 100;
    }

}
