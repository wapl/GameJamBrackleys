using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetDoor()
    {
        GetComponentInParent<Door>().ResetDoors();
    }
    public void IdleEnemy()
    {
        GetComponent<Animator>().SetBool("Reset", false);
    }
}
