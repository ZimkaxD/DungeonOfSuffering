using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public DefaultBullet defaultBullet;
    public PlayerStats playerStats;
    public int upgradeCost;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(UpLvl);
    }

   

    public void UpLvl()
    {
        if(playerStats.currentMoney>=upgradeCost)
        {
            playerStats.currentMoney-=upgradeCost;
            defaultBullet.lvl+=1;
            upgradeCost*=2;
        }

    }
}

