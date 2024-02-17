using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    
    public int doorId;
    public string behindTheDoor = "";
    public  Boolean revealed = false;
    
    public AudioSource audioSource;
    public AudioClip[] doorCroaks;
    public AudioClip[] rewardsSounds;
    public AudioClip[] enemiesSounds;
    public AudioClip ghoulReveal;
    public AudioClip trapReveal;
    
    public  GameObject player;

    public Sprite[] rewards;
    public Sprite[] Enemies;

    private Boolean selected;
    private string reward;
    public  string enemy;
    // Start is called before the first frame update
    void Start()
    {

        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        int i;
        i = UnityEngine.Random.Range(0, rewards.Length);
        transform.Find("Prize").GetComponent<SpriteRenderer>().sprite = rewards[i];
        switch(i)
        {
            case 0:
                reward = "Dog Treat";
                break;
            case 1:
                reward = "Night Vision";
                break;
            case 2:
                reward = "Bow";
                break;
            case 3:
                reward = "Musket";
                break;
            case 4:
                reward = "One Handed Swrod";
                break;
            case 5:
                reward = "Club";
                break;

        }
        i = UnityEngine.Random.Range(0, Enemies.Length);
        transform.Find("Enemy").GetComponent<SpriteRenderer>().sprite = Enemies[i];
        switch (i)
        {
            case 0:
                enemy = "Barbarian";
                break;
            case 1:
                enemy = "Wolf";
                break;
            case 2:
                enemy = "Zombie";
                break;

        }
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //Debug.Log("clicked"+DoorType);
        if(!revealed && GetComponentInParent<DoorsManager>().clicks==0)
        {
            
            GetComponentInParent<DoorsManager>().Reveal(doorId);
                //GetComponentInParent<DoorsManager>().OpenDialog();
            
            
        }
        else if (!revealed && GetComponentInParent<DoorsManager>().clicks == 1)
        {
            
            OpenDoor();
        }

        
        
    }
    public void playOpenSound()
    {
        int i = UnityEngine.Random.Range(0, doorCroaks.Length);
        audioSource.clip = doorCroaks[i];
        audioSource.Play();
    }

    public void playGhoulSound()
    {
        audioSource.clip = ghoulReveal;
        audioSource.Play();
    }

    public void playTrapSound()
    {
        audioSource.clip = trapReveal;
        audioSource.Play();
        
    }

    public void playEndSound()
    {
        if (selected)
        {
            if (behindTheDoor == "prize")
            {
                int i;
                i = UnityEngine.Random.Range(0, rewardsSounds.Length);
                audioSource.clip = rewardsSounds[i];
                audioSource.Play();

                transform.Find("Prize").gameObject.SetActive(true);

                player.GetComponent<Player>().addScore(10);
            }
            else if(behindTheDoor=="enemy")
            {
                switch (enemy)
                {
                    case "Barbarian":
                        audioSource.clip = enemiesSounds[0];
                        audioSource.Play();
                        break;
                    case "Wolf":
                        audioSource.clip = enemiesSounds[1];
                        audioSource.Play();
                        break;
                    case "Zombie":
                        audioSource.clip = enemiesSounds[2];
                        audioSource.Play();
                        break;
                }
                transform.Find("Enemy").gameObject.SetActive(true);

            }
            else
            {
                transform.Find("Trap").gameObject.SetActive(true);
                audioSource.clip = trapReveal;
                audioSource.Play();
            }
        }
        
    }
    
    public void OpenDoor()
    {
       
        GetComponent<Animator>().SetBool("DoorOpen", true);
        selected = true;
    }

    
}
