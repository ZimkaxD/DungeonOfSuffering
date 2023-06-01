using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuy : MonoBehaviour
{
    #region  SIngleton:ShopBuy

    public static ShopBuy Instance;
    void Awake()
    {
        if(Instance==null)
        {
            Instance=this;
            
        }

    }
    #endregion

    public PlayerStats playerStats;
    public GameObject Hero;
    public Text currentMoneyText;

    public void Start()
    {
         
        Hero = GameObject.Find("Hero");
        playerStats= Hero.GetComponent<PlayerStats>();
    }
    public void Update()
    {

        playerStats= Hero.GetComponent<PlayerStats>();
    }
    public void UseCoins(int amount)
    {
        playerStats.currentMoney-=amount;
        currentMoneyText.text=playerStats.currentMoney.ToString();
    }

    public bool HasEnoughCoins(int amount)
    {
        return(playerStats.currentMoney>=amount);
    }
}
