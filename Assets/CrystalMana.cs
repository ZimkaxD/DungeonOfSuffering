using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalMana : MonoBehaviour
{
    public GameObject Hero;
    public float magniteSpeed = 5f;

    void Start()
    {
        Hero = GameObject.Find("Hero");
    }


    void Update()
    {
        PlayerStats playerStats = Hero.GetComponent<PlayerStats>();

        float distance = Vector2.Distance(Hero.transform.position, transform.position);

        if (Hero != null)
        {
            if (distance < 3f)
            {
                Vector2 direction = Hero.transform.position - transform.position;
                transform.position = Vector2.MoveTowards(transform.position, Hero.transform.position, magniteSpeed * Time.deltaTime);
                if (distance < 0.5f)
                {
                    if (playerStats.currentMana < playerStats.maxMana)
                    {
                        playerStats.currentMana += 1;
                        Destroy(gameObject);
                    }
                    else
                    {
                        playerStats.currentMana += 0;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
