using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    public GameObject Hero;
    public int heal;

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
                if (playerStats.currentHealth >= playerStats.maxHealth)
                {
                    playerStats.currentHealth += 0;
                    Destroy(gameObject);
                }
                else
                {
                    playerStats.currentHealth += heal;
                    Destroy(gameObject);
                }
            }
        }
    }
    
}
