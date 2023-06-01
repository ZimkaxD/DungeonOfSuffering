using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public AchieveManager achieveManager;
    public int hp = 5;

    void Start()
    {
         achieveManager = FindObjectOfType<AchieveManager>();
    }


    void Update()
    {
        if(hp<=0)
        {
            Destroy(gameObject);
            achieveManager.ShowAchievement();
        }
    }
    public void TakeDamage(int damage)
    {
        hp-=damage;
        Debug.Log("Убил");
    }

}
