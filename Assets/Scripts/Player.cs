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

    public GameObject EndScreen;
    public TextMeshProUGUI SnifferUI;
    public TextMeshProUGUI NightVisionUi;

    public GameObject WeaponIconUi;
    public Sprite[] weapons;
    public DoorsManager doorsManager;
    public GameObject GameManager;
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
        
        doorsManager.playerUsedSnif = true;
    }

    public void useNightVision()
    {
       

        doorsManager.playerUsedNightVision= true;
    }
    public void removeSniff()
    {
        sniifferCharges--;
        SnifferUI.text = sniifferCharges.ToString();
    }

    public void removeNight()
    {
        nightVisionCharges--;
        NightVisionUi.text = nightVisionCharges.ToString();
    }
    public void addScore(int i)
    {
       
        this.score = score + i;
        
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
        SnifferUI.text = sniifferCharges.ToString();
    }

    public void addNightVision()
    {
        nightVisionCharges++;
        NightVisionUi.text = nightVisionCharges.ToString();

    }
    public void playerDamaged(float damage)
    {
        health = health - damage;
        //audioSource.clip = playerHurt;
        //audioSource.Play();
        HealthBar.value = health / 100;
        if (health <= 0)
        {
            EndScreen.SetActive(true);
            GameManager.GetComponent<GameManager>().Gameover = true;
        }
    }

    public void setWeapon(string equipment)
    {
        equppedWeapon = equipment;
        switch (equppedWeapon)
        {
            case "Bow":
                WeaponIconUi.GetComponent<SpriteRenderer>().sprite = weapons[0];
                break;
            case "Musket":
                WeaponIconUi.GetComponent<SpriteRenderer>().sprite = weapons[1];
                break;
            case "One Handed Sword":
                WeaponIconUi.GetComponent<SpriteRenderer>().sprite = weapons[2];
                break;
            case "Club":
                WeaponIconUi.GetComponent<SpriteRenderer>().sprite = weapons[3];
                break;
            case "":
                WeaponIconUi.GetComponent<SpriteRenderer>().sprite = null;
                break;
        }
    }

}
