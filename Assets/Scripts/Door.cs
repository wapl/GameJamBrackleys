using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Door : MonoBehaviour
{

    
    public int doorId;
    public string behindTheDoor = "";
    public  Boolean revealed = false;
    
    public AudioSource audioSource;
    public AudioSource AttackSource;
    public AudioClip[] doorCroaks;
    public AudioClip[] rewardsSounds;
    public AudioClip[] enemiesSounds;
    public AudioClip[] attackSounds;
    public AudioClip ghoulReveal;
    public AudioClip trapReveal;
    
    public  GameObject player;

    public Sprite[] rewards;
    public Sprite[] Enemies;

    public AnimatorController[] EnemyController;

    private Boolean selected;
    private string reward;
    public  string enemy;

    public GameObject GameManager;
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
                reward = "One Handed Sword";
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
                this.gameObject.transform.Find("Enemy").GetComponent<Animator>().runtimeAnimatorController = EnemyController[0];
                break;
            case 1:
                enemy = "Wolf";
                this.gameObject.transform.Find("Enemy").GetComponent<Animator>().runtimeAnimatorController = EnemyController[1];
                break;
            case 2:
                enemy = "Zombie";
                this.gameObject.transform.Find("Enemy").GetComponent<Animator>().runtimeAnimatorController = EnemyController[2];
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
         if(!GameManager.GetComponent<GameManager>().Gameover)
        {
            GetComponentInParent<DoorsManager>().Reveal(doorId);
        }
           
                //GetComponentInParent<DoorsManager>().OpenDialog();
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
                switch (reward)
                {
                    case "Dog Treat":
                        player.GetComponent<Player>().addSniffer();
                        break;
                    case "Night Vision":
                        player.GetComponent<Player>().addNightVision();
                        break;
                    case "Bow":
                        player.GetComponent<Player>().setWeapon("Bow");
                        break;
                    case "Musket":
                        player.GetComponent<Player>().setWeapon("Musket");
                        break;
                    case "One Handed Sword":
                        player.GetComponent<Player>().setWeapon("One Handed Sword");
                        break;
                    case "Club":
                        player.GetComponent<Player>().setWeapon("Club");
                        break;
                }

                transform.Find("Prize").gameObject.SetActive(true);

                player.GetComponent<Player>().addScore(10);
                StartCoroutine(ResetDelayed());
                //ResetDoors();
            }
            else if(behindTheDoor=="enemy")
            {
                
                transform.Find("Enemy").gameObject.SetActive(true);
                combat();

            }
            else
            {
                transform.Find("Trap").gameObject.SetActive(true);
                audioSource.clip = trapReveal;
                audioSource.Play();
                player.GetComponent<Player>().playerDamaged(10);
                StartCoroutine(ResetDelayed());
                //ResetDoors();
            }
        }
        
    }
    
    public void OpenDoor()
    {
       
        GetComponent<Animator>().SetBool("DoorOpen", true);
        selected = true;
    }
    public void combat()
    {
        float EnemyAttackValue = 0.0f;
        switch (enemy)
        {
            case "Barbarian":
                EnemyAttackValue = 3.0f;
                audioSource.clip = enemiesSounds[0];
               
                break;
            case "Wolf":
                EnemyAttackValue = 1.0f;
                audioSource.clip = enemiesSounds[1];
               
                break;
            case "Zombie":
                EnemyAttackValue = 2.0f;
                audioSource.clip = enemiesSounds[2];
                
                break;
        }
        if (player.GetComponent<Player>().equppedWeapon=="")
        {
            this.gameObject.transform.Find("Enemy").GetComponent<Animator>().SetBool("Has Won", true);
            int i;
            i = UnityEngine.Random.Range(0, 3);
          
            float damage = (float)((EnemyAttackValue + i) * 5.0);
            
            player.GetComponent<Player>().playerDamaged(damage);
            audioSource.Play();

        }
        else
        {
            float playerAttackValue = 0.0f;
            switch(player.GetComponent<Player>().equppedWeapon)
            {
                case "Bow":
                    playerAttackValue = 3.0f;
                    AttackSource.clip = attackSounds[0];
                    break;
                case "Musket":
                    playerAttackValue = 4.0f;
                    AttackSource.clip = attackSounds[2];
                    break;
                case "One Handed Sword":
                    playerAttackValue = 2.0f;
                    AttackSource.clip = attackSounds[3];
                    break;
                case "Club":
                    playerAttackValue = 1.0f;
                    AttackSource.clip = attackSounds[1];
                    break;
            }
            int playerRoll;
            playerRoll = UnityEngine.Random.Range(0, 6);
            float playerStrength=(float)((playerAttackValue + playerRoll)*5.0f);

            int enemyRoll;
            enemyRoll= UnityEngine.Random.Range(0, 6);
            float enemyStrength = (float)((EnemyAttackValue + enemyRoll) * 5.0f);
            if (playerStrength >= enemyStrength)
            {
                Debug.Log("Player Has Won");
                this.gameObject.transform.Find("Enemy").GetComponent<Animator>().SetBool("Has Won", false);
                AttackSource.Play();
            }
            else 
            {
                Debug.Log("Player Has Lost");
                audioSource.Play();
                this.gameObject.transform.Find("Enemy").GetComponent<Animator>().SetBool("Has Won", true);
                int i;
                i = UnityEngine.Random.Range(0, 3);
                float damage = (float)((EnemyAttackValue + i) * 5.0);
                player.GetComponent<Player>().playerDamaged(damage);
            }
            player.GetComponent<Player>().setWeapon("");
        }
    }
    public void ResetDoors()
    {
        GetComponentInParent<DoorsManager>().ResetDoors();
    }
    public void doorReset()
    {
        this.gameObject.transform.Find("Enemy").GetComponent<Animator>().SetBool("Reset", true);
        GetComponent<Animator>().SetBool("Reset", true);

        GetComponent<Animator>().SetBool("DoorOpen", false);
        GetComponent<Animator>().SetBool("DoorTrap", false);
        GetComponent<Animator>().SetBool("DoorReveal", false);

        transform.Find("Prize").gameObject.SetActive(false);
        transform.Find("Enemy").gameObject.SetActive(false);
        transform.Find("Trap").gameObject.SetActive(false);
        behindTheDoor = "";
        selected = false;
        revealed = false;
        reward = "";
        enemy = "";
        //GetComponent<Animator>().SetBool("Reset", false);
        //this.gameObject.transform.Find("Enemy").GetComponent<Animator>().SetBool("Reset", false);
    }
    public void DoorIdle()
    {
        GetComponent<Animator>().SetBool("Reset", false);
    }

    IEnumerator ResetDelayed()
    {
        yield return new WaitForSeconds(2);
        ResetDoors();
    }

}
