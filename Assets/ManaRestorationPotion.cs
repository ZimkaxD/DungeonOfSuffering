using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRestorationPotion : MonoBehaviour
{
    public GameObject Hero;
    public int manaRestore;

    void Start()
    {
        Hero = GameObject.Find("Hero");
    }

    
    void Update()
    {
        PlayerStats playerStats = Hero.GetComponent<PlayerStats>();
        if (Input.GetKeyDown(KeyCode.E))
        {

            float distance = Vector2.Distance(Hero.transform.position, transform.position);


            if (distance < 0.5f)
            {
                if (playerStats.currentMana >= playerStats.maxMana)
                {
                    playerStats.currentMana += 0;
                    Destroy(gameObject);
                }
                else
                {
                    playerStats.currentMana += manaRestore;
                    Destroy(gameObject);
                }
            }
        }
    }
}
