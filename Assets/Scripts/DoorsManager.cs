using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;


public class DoorsManager : MonoBehaviour
{
    [SerializeField] List<string> behindDoorScene;
    [SerializeField] List<string> behindDoorTemp;
    [SerializeField] GameObject[] doorPrefab;
    [SerializeField] AudioSource audioSource;
    public AudioSource dealer;
    [SerializeField] GameObject player;
    
    
    public AudioClip dogBark;
    public AudioClip wolfBark;
    public AudioClip[] dogSniffs;

    public  Boolean playerUsedSnif = false;
    public Boolean allowClick = true;
    public Boolean playerUsedNightVision = false;
    int randomIndex;
    public int clicks;

    [SerializeField] GameObject dialogPanel;

   

    // Start is called before the first frame update
    void Start()
    {
        Boolean prizeGenerated = false;
        clicks = 0;
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        while(!prizeGenerated)
        {
            foreach (GameObject go in doorPrefab)
            {
                do
                {
                    randomIndex = UnityEngine.Random.Range(0, behindDoorScene.Count);

                } while (behindDoorScene[randomIndex] == "");
                go.GetComponent<Door>().behindTheDoor = behindDoorScene[randomIndex];
                if (behindDoorScene[randomIndex] == "prize")
                {
                    behindDoorScene[randomIndex] = "";
                    prizeGenerated = true;
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // This is the dialogue  that opens that prompts they if they want to maintain their choice 
    public void OpenDialog()
    {
        dialogPanel.gameObject.SetActive(true);
    }

    public void CloseDialog()
    {
        dialogPanel.gameObject.SetActive(false);
    }

    
    
    //This is fired after a player a pick the door and the dealer reveals what is behind a door. 
    public void Reveal(int chosenDoor)
    {
        allowClick = false;
        
        if(!playerUsedSnif && !playerUsedNightVision)
        {
            dealer.clip = wolfBark;
            dealer.Play();
            List<GameObject> tempDoors = new List<GameObject>();

            foreach (GameObject door in doorPrefab)
            {
                if (door.GetComponent<Door>().doorId != chosenDoor && door.GetComponent<Door>().behindTheDoor != "prize")
                {
                    tempDoors.Add(door);
                }

            }

            int i = UnityEngine.Random.Range(0, tempDoors.Count);
            
           
            /*
            doorPrefab[doorId].GetComponent<SpriteRenderer>().enabled = false;
            doorPrefab[doorId].transform.Find("Selected").gameObject.SetActive(true);
            doorPrefab[doorId].transform.Find("Selected").GetComponent<SpriteRenderer>().enabled = true;
            */
            //Enable the revealed door 
           
            if (tempDoors[i].GetComponent<Door>().behindTheDoor == "enemy")
            {
                tempDoors[i].GetComponent<Animator>().SetBool("DoorReveal", true);
            }
            else if(tempDoors[i].GetComponent<Door>().behindTheDoor == "traps")
            {
                tempDoors[i].GetComponent<Animator>().SetBool("DoorReveal", true);
            }


                    tempDoors[i].GetComponent<Door>().revealed = true;

            dialogPanel.GetComponent<DialogManager>().NextDialog();
            clicks++;
        }
        else if(playerUsedSnif)
        {
            if(player.GetComponent<Player>().sniifferCharges>0)
            {
                StartCoroutine(DogSniffReveal(chosenDoor));
            }
            
        }
        else
        {
            if(player.GetComponent<Player>().nightVisionCharges>0)
            {
                doorPrefab[chosenDoor].GetComponent<Animator>().SetBool("DoorTrap", true);
            }
            
        }

       
    }
   
    IEnumerator DogSniffReveal(int doorId)
    {
        int i = UnityEngine.Random.Range(0, dogSniffs.Length);


        audioSource.clip = dogSniffs[i];
        audioSource.Play();
        //Dog Sniff clip 
        yield return new  WaitForSeconds(2);

        if (doorPrefab[doorId].GetComponent<Door>().behindTheDoor== "enemy")
        {
            audioSource.clip = dogBark;
            audioSource.Play();
            
            yield return new WaitForSeconds(1);
            doorPrefab[doorId].GetComponent<Animator>().SetBool("DoorReveal", true);


        }

        playerUsedSnif = false;
        
    }

   
    
}
