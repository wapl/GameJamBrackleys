using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private int sniifferCharges = 1;
    [SerializeField] private int nightVisionCharges = 1;
    [SerializeField] TextMeshProUGUI textScore;

    [SerializeField]private  int score;

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

}
